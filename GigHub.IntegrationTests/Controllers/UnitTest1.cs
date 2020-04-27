using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.IntegrationTests.Extensions;
using GigHub.Persistance;
using NCrunch.Framework;
using NUnit.Framework;
using System;
using System.Linq;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp() 
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown() 
        {
            _context.Dispose();
        }

        // TODO : REview this test
        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUppcommingGigs()
        {
            var user = _context.Users.First();

            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            // Assert
            //result.ViewData.Model as 
        }
    }
}
