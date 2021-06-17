using FrontEnd.Controllers;
using FrontEnd.Interfaces;
using FrontEnd.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HolidayGeneratorTest
{
    public class HomeControllerTest
    {
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IRepositoryWrapper> mockRepo;
        private HomeController homeController;

        public HomeControllerTest()
        {
            //controller setup
            mockRepo = new Mock<IRepositoryWrapper>();
            mockConfiguration = new Mock<IConfiguration>();
            homeController = new HomeController(mockConfiguration.Object, mockRepo.Object);
        }

        [Fact]
        public void IndexAsync_Test()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Results.Create(It.IsAny<Result>())).Returns(It.IsAny<Result>());

            //Act
            var controllerActionResult = homeController.IndexAsync();

            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<Task<IActionResult>>(controllerActionResult);

        }

        [Fact]
        public void StoreEntryInDatabase_Test()
        {
            //Arrange
            string month = "JUN";
            string days = "6";
            string destination = "Spain";
            mockRepo.Setup(repo => repo.Results.Create(It.IsAny<Result>())).Returns(It.IsAny<Result>());

            //Act
            homeController.StoreEntryInDatabase(destination, month, days);

            //Assert - in this case, verify as it's a non-query scenario 
            mockRepo.Verify(repo => repo.Results.Create(It.IsAny<Result>()), Times.Once());
            mockRepo.Verify(repo => repo.Save(), Times.Once());



        }
    }
}
