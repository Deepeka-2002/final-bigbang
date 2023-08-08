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
    public class BookingControllerTests
    {
        [Fact]
        public async Task GetAppointments_Returns_OkResultWithListOfAppointments()
        {
            // Arrange
            var bookings = new List<Bookings>
            {
                new Bookings { BookingId = 1, Name = "John Doe", Email = "john.doe@example.com" },
                new Bookings { BookingId = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
            };

            var bookingServiceMock = new Mock<IBooking>();
            bookingServiceMock.Setup(service => service.GetAppointments()).ReturnsAsync(bookings);

            var controller = new BookingController(bookingServiceMock.Object);

            // Act
            var result = await controller.GetAppointments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Bookings>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task AddAppointment_Returns_OkResultWithAddedAppointment()
        {
            // Arrange
            var newAppointment = new Bookings { BookingId = 3, Name = "New Customer", Email = "new.customer@example.com" };

            var bookingServiceMock = new Mock<IBooking>();
            bookingServiceMock.Setup(service => service.AddAppointment(It.IsAny<Bookings>()))
                .ReturnsAsync((Bookings appointment) =>
                {
                    // Add the new appointment to the list and return the updated list
                    var updatedList = new List<Bookings>
                    {
                        newAppointment
                    };
                    return updatedList;
                });

            var controller = new BookingController(bookingServiceMock.Object);

            // Act
            var result = await controller.AddAppointment(newAppointment);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Bookings>>(okResult.Value);
            Assert.Single(model);
            var appointment = model.First();
            Assert.Equal(3, appointment.BookingId);
            Assert.Equal("New Customer", appointment.Name);
            Assert.Equal("new.customer@example.com", appointment.Email);
        }

        [Fact]
        public async Task UpdateAppointment_ValidId_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            var updatedAppointment = new Bookings
            {
                BookingId = id,
                Name = "John Doe",
                Email = "john.doe@example.com",
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(5),
                Adult = 2,
                Child = 1
            };

            var bookingServiceMock = new Mock<IBooking>();
            bookingServiceMock.Setup(service => service.UpdateAppointmentById(id, It.IsAny<Bookings>()))
                .ReturnsAsync((int appointmentId, Bookings appointment) =>
                {
                    // Simulate updating the appointment in the database and returning the updated appointment.
                    updatedAppointment.Name = appointment.Name;
                    updatedAppointment.Email = appointment.Email;
                    updatedAppointment.CheckIn = appointment.CheckIn;
                    updatedAppointment.CheckOut = appointment.CheckOut;
                    updatedAppointment.Adult = appointment.Adult;
                    updatedAppointment.Child = appointment.Child;
                    return new List<Bookings> { updatedAppointment };
                });

            var controller = new BookingController(bookingServiceMock.Object);

            // Act
            var result = await controller.UpdateAppointment(id, updatedAppointment);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateAppointment_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var updatedAppointment = new Bookings
            {
                BookingId = id,
                Name = "John Doe",
                Email = "john.doe@example.com",
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(5),
                Adult = 2,
                Child = 1
            };

            var bookingServiceMock = new Mock<IBooking>();
            bookingServiceMock.Setup(service => service.UpdateAppointmentById(id, It.IsAny<Bookings>()))
                .ThrowsAsync(new ArithmeticException("Invalid id to update details"));

            var controller = new BookingController(bookingServiceMock.Object);

            // Act
            var result = await controller.UpdateAppointment(id, updatedAppointment);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteAppointmentById_Returns_OkResultWithDeletedAppointment()
        {
            // Arrange
            var existingAppointment = new Bookings { BookingId = 1, Name = "John Doe", Email = "john.doe@example.com" };

            var bookingServiceMock = new Mock<IBooking>();
            bookingServiceMock.Setup(service => service.DeleteAppointmentById(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    // Simulate deleting the appointment from the database and returning the list of remaining appointments.
                    var remainingAppointments = new List<Bookings>(); // You can add other appointments if needed.
                    return remainingAppointments;
                });

            var controller = new BookingController(bookingServiceMock.Object);

            // Act
            var result = await controller.DeleteAppointmentById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Bookings>>(okResult.Value);
            Assert.Empty(model); // Assert that the list of remaining appointments is empty.
        }
    }
}
