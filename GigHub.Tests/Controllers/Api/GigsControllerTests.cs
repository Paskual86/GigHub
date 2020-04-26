using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistance;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{

    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;


        [TestInitialize]
        public void InitializateTest() 
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUow.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "noah@mail.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId };
            gig.Id = 1;
            gig.Cancel();
            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
