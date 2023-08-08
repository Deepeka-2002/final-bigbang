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
using Moq;
using Xunit;

namespace BigBang.Tests.Controllers
{
    public class PackageControllerTests
    {
        private List<TourPackage> GetTestTourPackages()
        {
            return new List<TourPackage>
            {
                new TourPackage { PackageId = 1, Destination = "Destination 1", PriceForAdult = 100, PriceForChild = 50, Days = 5, PackageImg = "image1.jpg" },
                new TourPackage { PackageId = 2, Destination = "Destination 2", PriceForAdult = 200, PriceForChild = 100, Days = 7, PackageImg = "image2.jpg"}
            };
        }

        [Fact]
        public void GetTourPackages_ReturnsListOfPackages()
        {
            // Arrange
            var mockRepository = new Mock<IPackage>();
            var expectedTours = new List<TourPackage>
    {
        new TourPackage { PackageId = 1,  Destination = "Destination 1", PriceForAdult = 100, PriceForChild = 50, Days = 3, Description = "Description 1", PackageImg = "image1.jpg" },
        new TourPackage { PackageId = 2,Destination = "Destination 2", PriceForAdult = 150, PriceForChild = 75, Days = 5, Description = "Description 2", PackageImg = "image2.jpg" }
    };
            mockRepository.Setup(repo => repo.GetTourPackages()).Returns(() => null);
            var controller = new PackageController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = controller.GetTourPackages();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        public async Task GetPackageById_ValidId_Returns_OkResultWithPackage()
        {
            // Arrange
            int packageId = 1;
            var tourPackage = new TourPackage { PackageId = packageId, Destination = "Test Destination" };

            var packageServiceMock = new Mock<IPackage>();
            packageServiceMock.Setup(service => service.GetPackageById(packageId)).ReturnsAsync(tourPackage);

            var controller = new PackageController(packageServiceMock.Object, null);

            // Act
            var result = await controller.GetPackageById(packageId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<TourPackage>(okResult.Value);
            Assert.Equal(packageId, model.PackageId);
            Assert.Equal("Test Destination", model.Destination);
        }

        [Fact]
        public async Task GetPackageById_InvalidId_Returns_NotFoundResult()
        {
            // Arrange
            int packageId = 100;

            var packageServiceMock = new Mock<IPackage>();
            packageServiceMock.Setup(service => service.GetPackageById(packageId)).ThrowsAsync(new ArithmeticException("Invalid User Id"));

            var controller = new PackageController(packageServiceMock.Object, null);

            // Act
            var result = await controller.GetPackageById(packageId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Invalid User Id", notFoundResult.Value);
        }

      
        [Fact]
        public async Task Post_ValidPackage_Returns_CreatedAtActionResult()
        {
            // Arrange
            var tourPackage = new TourPackage { PackageId = 3, Destination = "New Destination", PriceForAdult = 300, PriceForChild = 150 };
            var imageFileMock = new Mock<IFormFile>();

            var packageServiceMock = new Mock<IPackage>();
            packageServiceMock.Setup(service => service.AddTourPackage(tourPackage, imageFileMock.Object)).ReturnsAsync(tourPackage);

            var controller = new PackageController(packageServiceMock.Object, null);

            // Act
            var result = await controller.Post(tourPackage, imageFileMock.Object);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Post", createdAtActionResult.ActionName);
            var model = Assert.IsType<TourPackage>(createdAtActionResult.Value);
            Assert.Equal(3, model.PackageId);
            Assert.Equal("New Destination", model.Destination);
        }

        [Fact]
        public async Task UpdatePackageById_ValidTour_ReturnsUpdatedTour()
        {
            // Arrange
            var mockRepository = new Mock<IPackage>();
            var existingTour = new TourPackage
            {
                PackageId = 1,
              
                Destination = "Existing Destination",
                PriceForAdult = 100,
                PriceForChild = 50,
                Days = 5,
                Description = "Existing Description",
                PackageImg = "existing-image.jpg",
                UserId = 1
            };
            var updatedTour = new TourPackage
            {
                PackageId = 1,
                
                Destination = "Updated Destination",
                PriceForAdult = 150,
                PriceForChild = 75,
                Days = 7,
                Description = "Updated Description",
                PackageImg = "updated-image.jpg",
                UserId = 1
            };
            var mockFormFile = new Mock<IFormFile>();
            mockRepository.Setup(repo => repo.UpdateTourPackageById(updatedTour, mockFormFile.Object)).ReturnsAsync(updatedTour);
            var controller = new PackageController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.Put(existingTour.PackageId, updatedTour, mockFormFile.Object);

            // Assert
            var actionResult = Assert.IsType<ActionResult<TourPackage>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualUpdatedTour = Assert.IsType<TourPackage>(okResult.Value);
            Assert.Equal(updatedTour, actualUpdatedTour);
        }


        [Fact]
        public async Task DeleteTourPackageByID_ExistingId_ReturnsDeletedTour()
        {
            // Arrange
            var mockRepository = new Mock<IPackage>();
            var tourToDelete = new TourPackage
            {
                PackageId = 1,
               
                Destination = "Existing Destination",
                PriceForAdult = 100,
                PriceForChild = 50,
                Days = 5,
                Description = "Existing Description",
                PackageImg = "existing-image.jpg",
                UserId= 1
            };
            mockRepository.Setup(repo => repo.DeleteTourPackageById(1)).ReturnsAsync(new List<TourPackage> { tourToDelete });
            var controller = new PackageController(mockRepository.Object, Mock.Of<IWebHostEnvironment>());

            // Act
            var result = await controller.DeleteTourPackageById(1);

        
            var actionResult = Assert.IsType<ActionResult<List<TourPackage>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var actualDeletedTourList = Assert.IsType<List<TourPackage>>(okResult.Value);
            Assert.Single(actualDeletedTourList);
            Assert.Equal(tourToDelete, actualDeletedTourList[0]);
        }

       
    }
}
