using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                g.Genre.Name.Contains(query) ||
                g.Venue.Contains(query));
            }

            var attendances = _context.Attendances.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList().ToLookup(a => a.GigId);
            var followees = _context.Followings.Where(a => a.FollowerId == userId).ToList().ToLookup(a => a.FolloweeId);

            var viewModel = new GigsViewModel
            {
                UpcommingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcomming Gigs",
                SearchTerm = query,
                Attendances = attendances,
                Followees = followees
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}