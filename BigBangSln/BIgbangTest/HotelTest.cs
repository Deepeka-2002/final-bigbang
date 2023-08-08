using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBang.Controllers;
using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using Xunit;

namespace BigBang.Tests.Controllers
{
    public class HotelsControllerTests
    {
        private List<Hotels> GetTestHotels()
        {
            return new List<Hotels>
            {
                new Hotels { HotelId = 1, HotelName = "Hotel 1", Location = "Location 1", HotelImg = "hotel1.jpg" },
                new Hotels { HotelId = 2, HotelName = "Hotel 2", Location = "Location 2", HotelImg = "hotel2.jpg" }
            };
        }

        [Fact]
        public void GetAllImages_ReturnsListOfHotels()
        {
            // Arrange
            var mockRepository = new Mock<IHotels>();
            var expectedTours = new List<Hotels>
    {
        new Hotels { HotelId = 1, HotelName = "Tour 1", Location = "Destination 1",  HotelImg = "image1.jpg" },
        new Hotels { HotelId = 2, HotelName = "Tour 2", Location = "Destination 2", HotelImg = "image2.jpg" }
    };

            mockRepository.Setup(repo => repo.GetHotels()).Returns(() => null);
            var controller = new HotelsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetHotels();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }




        [Fact]
        public async Task Filterpackage_ValidPackageId_ReturnsListOfHotels()
        {
            // Arrange
            int packageId = 1;
            var hotels = GetTestHotels().Where(h => h.PackageId == packageId).ToList();

            var hotelServiceMock = new Mock<IHotels>();
            hotelServiceMock.Setup(service => service.Filterpackage(packageId)).Returns(hotels);

            var controller = new HotelsController(hotelServiceMock.Object, null);

            // Act
            var result = controller.Filterpackage(packageId);

            // Assert
            var okResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Hotels>>(okResult.Value);

            // Check if the image property is a string in the model
            foreach (var item in model)
            {
                Assert.IsType<string>(item.HotelImg);
            }

            Assert.Equal(hotels.Count, model.Count());
        }

        [Fact]
        public async Task Post_ValidHotel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var hotel = new Hotels { HotelId = 3, HotelName = "New Hotel", Location = "New Location" };
            var imageFileMock = new Mock<IFormFile>();

            var hotelServiceMock = new Mock<IHotels>();
            hotelServiceMock.Setup(service => service.AddHotel(hotel, imageFileMock.Object)).ReturnsAsync(hotel);

            var controller = new HotelsController(hotelServiceMock.Object, null);

            // Act
            var result = await controller.Post(hotel, imageFileMock.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Post", createdAtActionResult.ActionName);
            var model = Assert.IsType<Hotels>(createdAtActionResult.Value);
            Assert.Equal(3, model.HotelId);
            Assert.Equal("New Hotel", model.HotelName);
            Assert.Equal("New Location", model.Location);
        }

        [Fact]
        public async Task Put_ValidHotel_ReturnsUpdatedHotel()
        {
            // Arrange
            var mockRepository = new Mock<IHotels>();
            var existingHotel = new Hotels
            {
                HotelId = 1,
                HotelName = "Existing Hotel",
                Location = "Existing Location",
                HotelImg = "existing-image.jpg",
                PackageId = 1
            };
            var updatedHotel = new Hotels
            {
                HotelId = 1,
                HotelName = "Updated Hotel",
                Location = "Updated Location",
                HotelImg = "updated-image.jpg",
                PackageId = 1
            };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.UpdateHotelById(updatedHotel, mockFormFile.Object)).ReturnsAsync(updatedHotel);
            var controller = new HotelsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.Put(existingHotel.HotelId, updatedHotel, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Hotels>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedHotel = Assert.IsType<Hotels>(okResult.Value);
            Assert.Equal(updatedHotel, actualUpdatedHotel);
        }

        [Fact]
        public async Task DeleteHotelById_ExistingId_ReturnsDeletedHotelList()
        {
            // Arrange
            var mockRepository = new Mock<IHotels>();
            var hotelToDelete = new Hotels
            {
                HotelId = 1,
                HotelName = "Existing Hotel",
                Location = "Existing Location",
                HotelImg = "existing-image.jpg",
                PackageId = 1
            };
            mockRepository.Setup(repo => repo.DeleteHotelById(1)).ReturnsAsync(new List<Hotels> { hotelToDelete });
            var controller = new HotelsController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.DeleteHotelById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Hotels>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualDeletedHotelList = Assert.IsType<List<Hotels>>(okResult.Value);
            Assert.Single(actualDeletedHotelList);
            Assert.Equal(hotelToDelete, actualDeletedHotelList[0]);
        }
    }
}
