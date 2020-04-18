using GigHub.Models;
using GigHub.Repositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var upcomingGigs = GetUpcomingGigs();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                g.Genre.Name.Contains(query) ||
                g.Venue.Contains(query));
            }

            var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);
            var followees = GetFollowees(userId).ToLookup(a => a.FolloweeId);

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

        private List<Following> GetFollowees(string userId)
        {
            return _context.Followings.Where(a => a.FollowerId == userId).ToList();
        }

        private IQueryable<Gig> GetUpcomingGigs()
        {
            return _context.Gigs
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
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