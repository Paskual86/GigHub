using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistance.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(p => p.Name).IsRequired().HasMaxLength(255);
        }
    }
    
}