using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public FollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetFollowees(string userId)
        {
            return _context.Followings.Where(a => a.FollowerId == userId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="followeeId"></param>
        /// <returns></returns>
        public IEnumerable<Following> GetFollowing(string userId, string followeeId)
        {
            return _context.Followings.Where(a => a.FolloweeId == followeeId && a.FollowerId == userId).ToList();
        }
    }
}