using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(k => new { k.FollowerId, k.FolloweeId });
        }
    }
}