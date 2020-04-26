using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Ninject.Infrastructure.Language;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistance.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Notification> GetNewNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToEnumerable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public void MarkAsRead(string userId) 
        {
            var notifications = _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead).ToList();
            notifications.ForEach(n => n.Read());
        }
    }
}