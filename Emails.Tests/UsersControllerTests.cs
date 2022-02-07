using Emails.Controllers;
using Emails.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Emails.Tests
{
    public class UsersControllerTests
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
            var mockUsers = new Mock<IUsersService>();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);
            controller.ModelState.AddModelError("Test", "Test");

            // Act
            var result = await controller.Index(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetIndex_WithNullSubject_ShouldActionServiceEnumerable()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>(MockBehavior.Strict);
            var expected = GetTestUsers();
            mockUsers.Setup(r => r.GetUsersAsync(null))
                       .ReturnsAsync(expected)
                       .Verifiable();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Index(null);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsersDTO>>(actionResult.Value);
            Assert.Equal(expected.Length, model.Count());
            // FIXME: could assert other result property values here

            mockUsers.Verify(r => r.GetUsersAsync(null), Times.Once);
        }

        [Fact]
        public async Task GetIndex_WithSubject_ShouldViewServiceEnumerable()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>(MockBehavior.Strict);
            var expected = GetTestUsers();
            mockUsers.Setup(r => r.GetUsersAsync("test subject"))
                       .ReturnsAsync(expected)
                       .Verifiable();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Index("test subject");

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsersDTO>>(actionResult.Value);
            Assert.Equal(expected.Length, model.Count());
            // FIXME: could assert other result property values here

            mockUsers.Verify(r => r.GetUsersAsync("test subject"), Times.Once);
        }

        [Fact]
        public async Task GetIndex_WhenBadServiceCall_ShouldActionEmptyEnumerable()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>(MockBehavior.Strict);
            mockUsers.Setup(r => r.GetUsersAsync(null))
                       .ThrowsAsync(new Exception())
                       .Verifiable();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Index(null);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UsersDTO>>(actionResult.Value);
            Assert.Empty(model);
            mockUsers.Verify(r => r.GetUsersAsync(null), Times.Once);
        }

        [Fact]
        public async Task GetDetails_WithInvalidModelState_ShouldBadResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);
            controller.ModelState.AddModelError("Something", "Something");

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetDetails_WithNullId_ShouldBadResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetDetails_WhenBadServiceCall_ShouldInternalError()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>(MockBehavior.Strict);
            mockUsers.Setup(r => r.GetUserAsync(3))
                       .ThrowsAsync(new Exception())
                       .Verifiable();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Details(3);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError,
                         statusCodeResult.StatusCode);
            mockUsers.Verify(r => r.GetUserAsync(3), Times.Once);
        }

        [Fact]
        public async Task GetDetails_WithUnknownId_ShouldNotFound()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>(MockBehavior.Strict);
            mockUsers.Setup(r => r.GetUserAsync(13))
                       .ReturnsAsync((UsersDTO)null)
                       .Verifiable();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Details(13);

            // Assert
            var statusCodeResult = Assert.IsType<NotFoundResult>(result);
            mockUsers.Verify(r => r.GetUserAsync(13), Times.Once);
        }

        [Fact]
        public async Task GetDetails_WithId_ShouldActionServiceObject()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var mockUsers = new Mock<IUsersService>(MockBehavior.Strict);
            var expected = GetTestUsers().First();
            mockUsers.Setup(r => r.GetUserAsync(expected.Id))
                       .ReturnsAsync(expected)
                       .Verifiable();
            var controller = new UsersController(mockLogger.Object,
                                                   mockUsers.Object);

            // Act
            var result = await controller.Details(expected.Id);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<UsersDTO>(actionResult.Value);
            Assert.Equal(expected.Id, model.Id);

            mockUsers.Verify(r => r.GetUserAsync(expected.Id), Times.Once);
        }

    }
}
