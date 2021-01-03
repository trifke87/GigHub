using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController controller;
        private Mock<IAttendanceRepository> mockRepository;
        private string userId;

        [TestInitialize]
        public void TestInitialize()
        {
            mockRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(mockRepository.Object);
            userId = "1";
            
            controller = new AttendancesController(mockUoW.Object);
            controller.MockCurrentUser(userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_UserAttendingAGigForWhichHeHasAnAttendance_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            mockRepository.Setup(r => r.GetAttendance(1, userId)).Returns(attendance);

            var result = controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidTest_ShouldReturnOk()
        {
            var result = controller.Attend(new AttendanceDto {GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Delete_NoAttendanceWithGivenIdExist_ShouldReturnNotFound()
        {
            var result = controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Delete_ValidTest_ShouldReturnOk()
        {
            var attendance = new Attendance();
            mockRepository.Setup(r => r.GetAttendance(1, userId)).Returns(attendance);

            var result = controller.Delete(1);
            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var attendance = new Attendance();
            mockRepository.Setup(r => r.GetAttendance(1, userId)).Returns(attendance);

            var result = (OkNegotiatedContentResult<int>) controller.Delete(1);
            result.Content.Should().Be(1);
        }
    }
}
