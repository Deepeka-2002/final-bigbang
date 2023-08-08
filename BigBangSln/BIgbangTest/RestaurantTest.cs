using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BigBang.Controllers;
using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BigBang.Tests.Controllers
{
    public class RestaurantsControllerTests
    {
        private List<Restaurents> GetTestRestaurants()
        {
            return new List<Restaurents>
            {
                new Restaurents { RestaurentId = 1, RestaurentName = "Restaurant 1", Location = "Location 1", PackageId = 1, RestaurentImg = "image1.jpg" },
                new Restaurents { RestaurentId = 2, RestaurentName = "Restaurant 2", Location = "Location 2", PackageId = 2, RestaurentImg = "image2.jpg" }
            };
        }

        [Fact]
        public void GetRestaurents_ReturnsListOfRestaurants()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurents>();
            var expectedTours = new List<Restaurents>
    {
                new Restaurents { RestaurentId = 1, RestaurentName = "Restaurant 1", Location = "Location 1", PackageId = 1, RestaurentImg = "image1.jpg" },
                new Restaurents { RestaurentId = 2, RestaurentName = "Restaurant 2", Location = "Location 2", PackageId = 2, RestaurentImg = "image2.jpg" }
            };

            mockRepository.Setup(repo => repo.GetRestaurents()).Returns(() => null);
            var controller = new RestaurentsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetRestaurents();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }





        [Fact]
        public async Task Post_ValidRestaurant_Returns_CreatedAtActionResult()
        {
            // Arrange
            var restaurant = new Restaurents { RestaurentId = 3, RestaurentName = "New Restaurant", Location = "New Location", PackageId = 3 };
            var imageFileMock = new Mock<IFormFile>();

            var restaurantServiceMock = new Mock<IRestaurents>();
            restaurantServiceMock.Setup(service => service.AddRestaurent(restaurant, imageFileMock.Object)).ReturnsAsync(restaurant);

            var controller = new RestaurentsController(restaurantServiceMock.Object, null);

            // Act
            var result = await controller.Post(restaurant, imageFileMock.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Post", createdAtActionResult.ActionName);
            var model = Assert.IsType<Restaurents>(createdAtActionResult.Value);
            Assert.Equal(3, model.RestaurentId);
            Assert.Equal("New Restaurant", model.RestaurentName);
            Assert.Equal("New Location", model.Location);
            Assert.Equal(3, model.PackageId);
        }

        [Fact]
        public async Task UpdateRestaurantById_ValidRestaurant_ReturnsUpdatedRestaurant()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurents>();
            var existingRestaurant = new Restaurents
            {
                RestaurentId = 1,
                RestaurentName = "Existing Restaurant",
                Location = "Existing Location",
                PackageId = 1,
                RestaurentImg = "existing-image.jpg"
            };
            var updatedRestaurant = new Restaurents
            {
                RestaurentId = 1,
                RestaurentName = "Updated Restaurant",
                Location = "Updated Location",
                PackageId = 2,
                RestaurentImg = "updated-image.jpg"
            };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.UpdateRestaurentById(updatedRestaurant, mockFormFile.Object)).ReturnsAsync(updatedRestaurant);
            var controller = new RestaurentsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.Put(existingRestaurant.RestaurentId, updatedRestaurant, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Restaurents>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedRestaurant = Assert.IsType<Restaurents>(okResult.Value);
            Assert.Equal(updatedRestaurant, actualUpdatedRestaurant);
        }

        [Fact]
        public async Task DeleteRestaurantById_ExistingId_ReturnsDeletedRestaurant()
        {
            // Arrange
            var mockRepository = new Mock<IRestaurents>();
            var restaurantToDelete = new Restaurents
            {
                RestaurentId = 1,
                RestaurentName = "Existing Restaurant",
                Location = "Existing Location",
                PackageId = 1,
                RestaurentImg = "existing-image.jpg"
            };
            mockRepository.Setup(repo => repo.DeleteRestaurentById(1)).ReturnsAsync(new List<Restaurents> { restaurantToDelete });
            var controller = new RestaurentsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.DeleteRestaurentById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Restaurents>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualDeletedRestaurantList = Assert.IsType<List<Restaurents>>(okResult.Value);
            Assert.Single(actualDeletedRestaurantList);
            Assert.Equal(restaurantToDelete, actualDeletedRestaurantList[0]);
        }
    }
}
