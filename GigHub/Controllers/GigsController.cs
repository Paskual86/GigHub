using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController() 
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Attending() 
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
            
            var attendances = _context.Attendances.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList().ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel()
            {
                UpcommingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }


        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var viewModel = new GigsViewModel()
            {
                UpcommingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Artists I'm Following"
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Mine() 
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now && !g.IsCanceled). Include(g => g.Genre).ToList();
            return (View(gigs));
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(GigsViewModel viewModel) 
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Add a Gig",
                Genres = _context.Genres.ToList()
            };

            return View("GigForm", viewModel);
        }

        /// <summary>
        /// Update Gig
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);
            
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Id = gig.Id,
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel) 
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            _context.Gigs.Add(Gig.New(User.Identity.GetUserId(), viewModel.GetDateTime(), viewModel.Genre, viewModel.Venue));
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Modify(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);
            
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id) 
        {
            var gig = _context.Gigs
                                .Include(g => g.Artist)
                                .Include(g => g.Genre)
                                .SingleOrDefault(g => g.Id == id);
            if (gig == null) 
            {
                return HttpNotFound();
            }

            var viewModel = new GigDetailsViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated) 
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances.Any(a => a.GigId == gig.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings.Any(a => a.FolloweeId == gig.ArtistId && a.FollowerId == userId);
            }

            return View("Details", viewModel);
        }
    }
}