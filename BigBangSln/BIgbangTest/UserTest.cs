using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigBang.Controllers;
using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BigBang.Tests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task GetPendingUsers_Returns_OkResultWithListOfPendingUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 1, UserName = "John Doe", EmailId = "john.doe@example.com", Status = false },
                new User { UserId = 2, UserName = "Jane Smith", EmailId = "jane.smith@example.com", Status = false }
            };

            var userServiceMock = new Mock<IUsers>();
            userServiceMock.Setup(service => service.GetPendingUsers()).ReturnsAsync(users);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.GetPendingUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetActiveUsers_Returns_OkResultWithListOfActiveUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 3, UserName = "Mike Smith", EmailId = "mike.smith@example.com", Status = true },
                new User { UserId = 4, UserName = "Sarah Johnson", EmailId = "sarah.johnson@example.com", Status = true }
            };

            var userServiceMock = new Mock<IUsers>();
            userServiceMock.Setup(service => service.GetActiveUsers()).ReturnsAsync(users);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.GetActiveUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Register_Returns_OkResultWithJwtToken()
        {
            // Arrange
            var newUser = new User
            {
                UserId = 5,
                UserName = "New User",
                EmailId = "new.user@example.com",
                Password = "password123",
                Role = "Agent",
                Status = false
            };

            var userServiceMock = new Mock<IUsers>();
            userServiceMock.Setup(service => service.AddUser(It.IsAny<User>())).ReturnsAsync(newUser);

            var controller = new UsersController(userServiceMock.Object);

            // Act
            var result = await controller.Register(newUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var token = Assert.IsType<string>(okResult.Value);
            Assert.NotEmpty(token); // Assert that the token is not empty (JWT token should be generated successfully).
        }

        // Add more test methods for other actions in the UsersController as needed.
    }
}
