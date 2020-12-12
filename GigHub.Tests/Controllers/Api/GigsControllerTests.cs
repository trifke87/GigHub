using GigHub.Controllers;
using GigHub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController controller;
        public GigsControllerTests()
        {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user1@domain.com")
                );
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1")
                );

            var principal = new GenericPrincipal(identity, null);

            var mockUoW = new Mock<IUnitOfWork>();

            controller = new GigsController(mockUoW.Object);

            //controller.User = principal;
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
