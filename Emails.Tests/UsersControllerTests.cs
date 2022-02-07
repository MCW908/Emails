using Emails.Controllers;
using Emails.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Emails.Tests
{
    public class UsersControllerTests
    {
        private UsersDTO[] GetTestReviews() => new UsersDTO[] {
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
            controller.ModelState.AddModelError("Test", "Test");

            // Act
            var result = await controller.Index(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
