using GigHub.Core.Models;
using GigHub.Persistance;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    /*
     La version que utilizan de NUnit en el tutorial es la 2.6.3 No la 3
     */
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLastestVersion();

            Seed();
        }

        private static void MigrateDbToLastestVersion()
        {
            var configuration = new Migrations.Configuration();

            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed() 
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any()) return;

            context.Users.Add(new ApplicationUser { UserName = "user1", Id = "user1", PasswordHash = "-" });
            context.Users.Add(new ApplicationUser { UserName = "user2", Id = "user2", PasswordHash = "-" });
        }
    }
}
