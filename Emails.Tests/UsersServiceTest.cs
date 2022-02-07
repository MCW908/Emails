using Emails.Controllers;
using Emails.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// TODO: Further tests can be implemented when the Users container has been implemented and published to Microsoft Azure
/// </summary>


namespace Emails.Tests
{
    public class UsersServiceTest
    {
        private Mock<HttpMessageHandler> CreateHttpMock(HttpStatusCode expectedCode,
                                                        string expectedJson)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = expectedCode
            };
            if (expectedJson != null)
            {
                response.Content = new StringContent(expectedJson,
                                                     Encoding.UTF8,
                                                     "application/json");
            }
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response)
                .Verifiable();
            return mock;
        }

        private IUsersService CreateUsersService(HttpClient client)
        {
            var mockConfiguration = new Mock<IConfiguration>(MockBehavior.Strict);
            mockConfiguration.Setup(c => c["BaseUrls:UsersService"])
                             .Returns("http://example.com");
            return new UsersService(client, mockConfiguration.Object);
        }

        //[Fact]
        //public async Task GetUserAsync_WithValid_ShouldOkEntity()
        //{
        //    // Arrange
        //    var expectedResult = new UsersDTO { Id = 1, UName = "Tester123", Email = "t123@gmail.com ", Password = "password1234" };
        //    var expectedJson = JsonConvert.SerializeObject(expectedResult);
        //    var expectedUri = new Uri("http://example.com/api/Users/1");
        //    var mock = CreateHttpMock(HttpStatusCode.OK, expectedJson);
        //    var client = new HttpClient(mock.Object);
        //    var service = CreateUsersService(client);

        //    // Act
        //    var result = await service.GetUserAsync(1);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(expectedResult.Id, result.Id);
        //    // FIXME: could assert other result property values
        //    mock.Protected()
        //        .Verify("SendAsync",
        //                Times.Once(),
        //                ItExpr.Is<HttpRequestMessage>(
        //                    req => req.Method == HttpMethod.Get
        //                           && req.RequestUri == expectedUri),
        //                ItExpr.IsAny<CancellationToken>()
        //                );
        //}

        //[Fact]
        //public async Task GetUserAsync_WithInvalid_ShouldReturnNull()
        //{
        //    // Arrange
        //    var expectedUri = new Uri("http://example.com/api/users/100");
        //    var mock = CreateHttpMock(HttpStatusCode.NotFound, null);
        //    var client = new HttpClient(mock.Object);
        //    var service = CreateUsersService(client);

        //    // Act
        //    var result = await service.GetUserAsync(100);

        //    // Assert
        //    Assert.Null(result);
        //    mock.Protected()
        //        .Verify("SendAsync",
        //                Times.Once(),
        //                ItExpr.Is<HttpRequestMessage>(
        //                    req => req.Method == HttpMethod.Get
        //                           && req.RequestUri == expectedUri),
        //                ItExpr.IsAny<CancellationToken>()
        //                );
        //}

        [Fact]
        public async Task GetUserAsync_OnHttpBad_ShouldThrow()
        {
            // Arrange
            var expectedUri = new Uri("http://example.com/api/Users/1");
            var mock = CreateHttpMock(HttpStatusCode.ServiceUnavailable, null);
            var client = new HttpClient(mock.Object);
            var service = CreateUsersService(client);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(
                () => service.GetUserAsync(1));
        }

        [Fact]
        public async Task GetUsersAsync_WithNull_ShouldReturnAll()
        {
            // Arrange
            var expectedResult = new UsersDTO[] { 
        
                // Insert Seed Data for the fake
                new UsersDTO{ Id = 1, UName = "Tester123", Email = "t123@gmail.com ", Password = "password1234"},
                new UsersDTO{ Id = 2, UName = "MCaldwell00", Email = "mdcaldwell16@gmail.com", Password = "testpass"},
                new UsersDTO{ Id = 3, UName = "Joe_Bloggs", Email = "JBL@gmail.com", Password = "abcdef"},
                new UsersDTO{ Id = 4, UName = "AABB", Email = "AABB@gmail.com", Password = "AABB"}

            };
            var expectedJson = JsonConvert.SerializeObject(expectedResult);
            var expectedUri = new Uri("http://example.com/api/Users");
            var mock = CreateHttpMock(HttpStatusCode.OK, expectedJson);
            var client = new HttpClient(mock.Object);
            var service = CreateUsersService(client);

            // Act
            var result = (await service.GetUsersAsync(null)).ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult.Length, result.Length);
            for (int i = 0; i < result.Length; i++)
            {
                Assert.Equal(expectedResult[i].Id, result[i].Id);
            }
            mock.Protected()
                .Verify("SendAsync",
                        Times.Once(),
                        ItExpr.Is<HttpRequestMessage>(
                            req => req.Method == HttpMethod.Get
                                   && req.RequestUri == expectedUri),
                        ItExpr.IsAny<CancellationToken>()
                        );
        }

        //[Fact]
        //public async Task GetUsersAsync_WithValid_ShouldReturnList()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact]
        //public async Task GetUsersAsync_WithInvalid_ShouldReturnEmpty()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact]
        //public async Task GetUsersAsync_OnHttpBad_ShouldThrow()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
