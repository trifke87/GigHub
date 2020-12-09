using GigHub.Core.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GigRepository _gigRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
            _gigRepository = new GigRepository(_context);
        }
        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                                g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoaming Gigs",
                SearchTerm = query,
                Attendaces = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
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