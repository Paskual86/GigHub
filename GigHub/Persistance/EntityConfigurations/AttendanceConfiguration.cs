using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(c => new { c.GigId, c.AttendeeId });
        }
    }
}