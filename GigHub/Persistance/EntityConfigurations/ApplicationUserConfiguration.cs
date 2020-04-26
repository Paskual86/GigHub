using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(100);

            /*
              modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);
             */

            HasMany(u => u.Followers)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            HasMany(au => au.UserNotifications).WithRequired(u => u.User).WillCascadeOnDelete(false);            
        }
    }
}