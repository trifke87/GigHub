using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository repository;
        private Mock<DbSet<Gig>> mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            mockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(m => m.Gigs).Returns(mockGigs.Object);

            repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            mockGigs.SetSource(new List<Gig> { gig });

            var result = repository.GetUpcomingGigsByArtist("1");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();
            mockGigs.SetSource(new List<Gig> { gig });

            var result = repository.GetUpcomingGigsByArtist("1");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            mockGigs.SetSource(new List<Gig> { gig });

            var result = repository.GetUpcomingGigsByArtist(gig.ArtistId + "-");

            result.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigByArtist_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            mockGigs.SetSource(new List<Gig> { gig });

            var result = repository.GetUpcomingGigsByArtist(gig.ArtistId);

            result.Should().Contain(gig);
        }
    }
}
