using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class FollowRepository : IFollowRepository
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

        public void Create(Following following)
        {
            _context.Followings.Add(following);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="followerId"></param>
        /// <returns></returns>
        public IEnumerable<Following> GetFollowees(string followerId)
        {
            return _context.Followings.Where(a => a.FollowerId == followerId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="followerId"></param>
        /// <param name="followeeId"></param>
        /// <returns></returns>
        public IEnumerable<Following> GetFollowing(string followerId, string followeeId)
        {
            return _context.Followings.Where(a => a.FolloweeId == followeeId && a.FollowerId == followerId).ToList();
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="following"></param>
        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}