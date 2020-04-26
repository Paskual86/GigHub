using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface INotificationRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Notification> GetNewNotifications(string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        void MarkAsRead(string userId);
    }
}
