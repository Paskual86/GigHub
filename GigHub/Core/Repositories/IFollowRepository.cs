using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowRepository
    {
        IEnumerable<Following> GetFollowees(string userId);
        IEnumerable<Following> GetFollowing(string userId, string followeeId);
        void Create(Following following);
        void Remove(Following following);
    }
}