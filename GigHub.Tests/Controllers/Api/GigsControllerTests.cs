using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController controller;
        private Mock<IGigRepository> mockRepository;
        private string userId;
        public GigsControllerTests()
        {
            mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(mockRepository.Object);
            userId = "1";
            controller = new GigsController(mockUoW.Object);
            controller.MockCurrentUser(userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExist_ShouldReturnNotFound()
        {
            var result = controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = userId + "-"};

            mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }
    }
}
