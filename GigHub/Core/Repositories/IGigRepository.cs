using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGigRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gig"></param>
        void Add(Gig gig);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Gig> GetFutureGigsWithGenre(string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gigId"></param>
        /// <returns></returns>
        Gig GetGig(int gigId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Gig> GetUpcomingGigs();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gigId"></param>
        /// <returns></returns>
        Gig GetGigWithAttendees(int gigId);
    }
}