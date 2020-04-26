using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GigHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; }

        public ApplicationUser()
        {
            Followers = new Collection<Following>();
            Followees = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        public void Notify(Notification notification)
        {
            // Esto lo toma EF como un insert!
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}