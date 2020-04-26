using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Gig> GetGigWithArtistAndGenre(string userId)
        {
            return _context.Attendances.Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Attendance> GetAttendance(int gigId, string userId)
        {            
            return _context.Attendances.Where(a => a.GigId == gigId && a.AttendeeId == userId).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attendance"></param>
        public void Create(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attendance"></param>
        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}