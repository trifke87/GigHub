using GigHub.Models;
using GigHub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public GigRepository Gigs { get; private set; }
        public AttendanceRepository Attendances { get; set; }
        public GenreRepository Genres { get; set; }
        public FollowingRepository Followings { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Genres = new GenreRepository(_context);
            Followings = new FollowingRepository(_context);
        }

        public void Completed()
        {
            _context.SaveChanges();
        }
    }
}