using GigHub.Core.Models;
using GigHub.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string ArtistId, string userId)
        {
            return _context.Followings
                    .SingleOrDefault(f => f.FolloweeId == ArtistId && f.FollowerId == userId);
        }
    }
}