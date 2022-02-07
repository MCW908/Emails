using System;
using Xunit;
using Emails.Controllers;
using Emails.Services;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Emails.Tests
{
    public class UsersControllerTest
    {
        private UsersDTO[] GetTestUsers() => new UsersDTO[] {
            new UsersDTO{ Id = 1, UName = "Tester123", Email = "t123@gmail.com ", Password = "password1234"},
            new UsersDTO{ Id = 2, UName = "MCaldwell00", Email = "mdcaldwell16@gmail.com", Password = "testpass"},
            new UsersDTO{ Id = 3, UName = "Joe_Bloggs", Email = "JBL@gmail.com", Password = "abcdef"},
            new UsersDTO{ Id = 4, UName = "AABB", Email = "AABB@gmail.com", Password = "AABB"}
        };

        [Fact]
        public async Task GetIndex_WithInvalidModelState_ShouldBadResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockReviews = new Mock<IUsersService>();
            var controller = new UsersController(mockLogger.Object,
                                                   mockReviews.Object);
            controller.ModelState.AddModelError("Something", "Something");

            // Act
            var result = await controller.Index(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
