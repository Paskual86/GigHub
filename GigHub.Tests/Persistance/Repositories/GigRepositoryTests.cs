using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Tests.Persistance.Repositories
{
    /*
     Hay un tema con la version de mock, por eso utilizo la version 4.2.1510.2205. Segun lo que lei la verion mas actualizada no trae la implementacion 
    del IProvider. 
    Por otro lado hay un articulo de Microsoft que habla sobre el mock https://docs.microsoft.com/es-es/ef/ef6/fundamentals/testing/writing-test-doubles?redirectedfrom=MSDN#doubles
    Pero es muy compleja la implementacion. Revisar en detalle la implementacion de la libreria mock para mas detalles.

     */
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitializate()
        {
            _mockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcommingGigsByArtistId_GigIsInThePast_ShouldNotBeReturned() 
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId="1" };

            _mockGigs.SetSource(new [] { gig });

            var gigResponse = _repository.GetFutureGigsWithGenre("1");

            gigResponse.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcommingGigsByArtist_GigIsCanceled_ShouldNotBeReturned() 
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigResponse = _repository.GetFutureGigsWithGenre("1");

            gigResponse.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcommingGigsByArtist_GigIsForDifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigResponse = _repository.GetFutureGigsWithGenre("2");

            gigResponse.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcommingGigsByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });

            var gigResponse = _repository.GetFutureGigsWithGenre("1");

            gigResponse.Should().Contain(gig);
        }
    }
}
