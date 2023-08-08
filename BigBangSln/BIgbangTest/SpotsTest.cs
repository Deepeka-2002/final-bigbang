using System;
using System.Collections.Generic;
using System.IO;
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
    public class SpotsControllerTests
    {
        private List<NearbySpots> GetTestSpots()
        {
            return new List<NearbySpots>
            {
                new NearbySpots { SpotId = 1, Name = "Spot 1", Location = "Location 1", PackageId = 1, Spotsimg = "image1.jpg" },
                new NearbySpots { SpotId = 2, Name = "Spot 2", Location = "Location 2", PackageId = 2, Spotsimg = "image2.jpg" }
            };
        }

        [Fact]
        public void GetSpots_ReturnsListOfPackages()
        {
            // Arrange
            var mockRepository = new Mock<ISpots>();
            var expectedTours = new List<NearbySpots>
   {
                new NearbySpots { SpotId = 1, Name = "Spot 1", Location = "Location 1", PackageId = 1, Spotsimg = "image1.jpg" },
                new NearbySpots { SpotId = 2, Name = "Spot 2", Location = "Location 2", PackageId = 2, Spotsimg = "image2.jpg" }
            };
            mockRepository.Setup(repo => repo.GetSpots()).Returns(() => null);
            var controller = new SpotsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetSpots();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }



        [Fact]
        public async Task Post_ValidSpot_Returns_CreatedAtActionResult()
        {
            // Arrange
            var spot = new NearbySpots { SpotId = 3, Name = "New Spot", Location = "New Location", PackageId = 3 };
            var imageFileMock = new Mock<IFormFile>();

            var spotServiceMock = new Mock<ISpots>();
            spotServiceMock.Setup(service => service.AddSpots(spot, imageFileMock.Object)).ReturnsAsync(spot);

            var controller = new SpotsController(spotServiceMock.Object, null);

            // Act
            var result = await controller.Post(spot, imageFileMock.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Post", createdAtActionResult.ActionName);
            var model = Assert.IsType<NearbySpots>(createdAtActionResult.Value);
            Assert.Equal(3, model.SpotId);
            Assert.Equal("New Spot", model.Name);
            Assert.Equal("New Location", model.Location);
            Assert.Equal(3, model.PackageId);
        }

        [Fact]
        public async Task UpdateSpotById_ValidSpot_ReturnsUpdatedSpot()
        {
            // Arrange
            var mockRepository = new Mock<ISpots>();
            var existingSpot = new NearbySpots
            {
                SpotId = 1,
                Name = "Existing Spot",
                Location = "Existing Location",
                PackageId = 1,
                Spotsimg = "existing-image.jpg"
            };
            var updatedSpot = new NearbySpots
            {
                SpotId = 1,
                Name = "Updated Spot",
                Location = "Updated Location",
                PackageId = 2,
                Spotsimg = "updated-image.jpg"
            };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.UpdateSpotById(updatedSpot, mockFormFile.Object)).ReturnsAsync(updatedSpot);
            var controller = new SpotsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.Put(existingSpot.SpotId, updatedSpot, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<NearbySpots>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedSpot = Assert.IsType<NearbySpots>(okResult.Value);
            Assert.Equal(updatedSpot, actualUpdatedSpot);
        }

        [Fact]
        public async Task DeleteSpotById_ExistingId_ReturnsDeletedSpot()
        {
            // Arrange
            var mockRepository = new Mock<ISpots>();
            var spotToDelete = new NearbySpots
            {
                SpotId = 1,
                Name = "Existing Spot",
                Location = "Existing Location",
                PackageId = 1,
                Spotsimg = "existing-image.jpg"
            };
            mockRepository.Setup(repo => repo.DeleteSpotById(1)).ReturnsAsync(new List<NearbySpots> { spotToDelete });
            var controller = new SpotsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.DeleteSpotById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<NearbySpots>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualDeletedSpotList = Assert.IsType<List<NearbySpots>>(okResult.Value);
            Assert.Single(actualDeletedSpotList);
            Assert.Equal(spotToDelete, actualDeletedSpotList[0]);
        }
    }
}
