using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetAttendance(int gigId, string userId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        IEnumerable<Gig> GetGigWithArtistAndGenre(string userId);
        void Create(Attendance attendance);
        void Remove(Attendance attendance);
    }
}