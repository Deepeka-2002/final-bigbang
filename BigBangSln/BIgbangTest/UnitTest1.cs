using System.Collections.Generic;
using System.Threading.Tasks;
using BigBang.Controllers;
using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Xunit;

namespace BigBang.Tests.Controllers
{
    public class FeedbackControllerTests
    {
        [Fact]
        public async Task GetFeedbacks_Returns_OkResultWithListOfFeedbacks()
        {
            // Arrange
            var feedbacks = new List<Feedback>
            {
                new Feedback { FeedId = 1, Description = "Test 1" },
                new Feedback { FeedId = 2, Description = "Test 2" }
            };

            var feedbackServiceMock = new Mock<IFeedback>();
            feedbackServiceMock.Setup(service => service.GetFeedbacks()).ReturnsAsync(feedbacks);

            var controller = new FeedbackController(feedbackServiceMock.Object);

            // Act
            var result = await controller.GetFeedbacks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Feedback>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task AddFeedback_Returns_OkResultWithAddedFeedback()
        {
            // Arrange
            var newFeedback = new Feedback { FeedId = 3, Description = "New Test", UserId = 3 };

            var feedbackServiceMock = new Mock<IFeedback>();
            feedbackServiceMock.Setup(service => service.AddFeedback(It.IsAny<Feedback>())).ReturnsAsync(new List<Feedback> { newFeedback });

            var controller = new FeedbackController(feedbackServiceMock.Object);

            // Act
            var result = await controller.AddFeedback(newFeedback);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Feedback>>(okResult.Value);
            Assert.Single(model);
            Assert.Equal(3, model.First().FeedId);
            Assert.Equal("New Test", model.First().Description);
            Assert.Equal(3, model.First().UserId);
        }


        [Fact]
        public async Task UpdateFeedback_ValidId_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            Feedback updatedFeedback = new Feedback
            {
                FeedId = id, // Assuming FeedId is the correct field for the feedback ID
                Description = "Updated Test",
                UserId = 1, // Assuming UserId is the correct field for the user ID
                Name = "John Doe",
                Email = "john.doe@example.com",
                Rating = 5,
            };

            var feedbackServiceMock = new Mock<IFeedback>();
            feedbackServiceMock.Setup(service => service.UpdateFeedbackById(It.IsAny<int>(), It.IsAny<Feedback>()))
                               .ReturnsAsync((int feedbackId, Feedback feedback) =>
                               {
                                   // Simulate updating the feedback in the database and returning the updated feedback.
                                   var updatedFeedbackList = new List<Feedback>
                                   {
                               updatedFeedback
                                   };
                                   return updatedFeedbackList;
                               });

            var controller = new FeedbackController(feedbackServiceMock.Object);

            // Act
            var result = await controller.UpdateFeedback(id, updatedFeedback);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Feedback>>(okResult.Value);
            Assert.Single(model);
            var feedback = model.First();
            Assert.Equal(id, feedback.FeedId);
            Assert.Equal("Updated Test", feedback.Description);
            Assert.Equal(1, feedback.UserId);
            Assert.Equal("John Doe", feedback.Name);
            Assert.Equal("john.doe@example.com", feedback.Email);
            Assert.Equal(5, feedback.Rating);
        }


        [Fact]
        public async Task DeleteFeedbackById_Returns_OkResultWithDeletedFeedback()
        {
            // Arrange
            var existingFeedback = new Feedback { FeedId = 1, Description = "Test 1", UserId = 1 };

            var feedbackServiceMock = new Mock<IFeedback>();
            feedbackServiceMock.Setup(service => service.DeleteFeedbackById(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    // Simulate deleting the feedback from the database and returning the list of remaining feedback.
                    var remainingFeedback = new List<Feedback>(); // You can add other feedback items if needed.
                    return remainingFeedback;
                });

            var controller = new FeedbackController(feedbackServiceMock.Object);

            // Act
            var result = await controller.DeleteFeedbackById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Feedback>>(okResult.Value);
            Assert.Empty(model); // Assert that the list of remaining feedback is empty.
        }


    }
}
