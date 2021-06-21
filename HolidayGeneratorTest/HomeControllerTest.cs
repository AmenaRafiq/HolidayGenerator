using FrontEnd.Controllers;
using FrontEnd.Interfaces;
using FrontEnd.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HolidayGeneratorTest
{
    public class HomeControllerTest
    {
        private Mock<IRepositoryWrapper> mockRepo;
        private HomeController homeController;

        public HomeControllerTest()
        {
            //controller setup
            mockRepo = new Mock<IRepositoryWrapper>();
            
        }

        [Fact]
        public async void IndexAsync_Test()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            Environment.SetEnvironmentVariable("mergeServiceURL", "http://localhost:11273");
            //mockHttp.When("http://localhost:17829/days").Respond("text/plain", "6");
            //mockHttp.When("http://localhost:44717/month").Respond("text/plain", "JAN");
            mockHttp.When("http://localhost:11273/merge").Respond("text/plain", "Spain, JUN, 3");

            var client = new HttpClient(mockHttp);
            homeController = new HomeController(mockRepo.Object, client);
            mockRepo.Setup(repo => repo.Results.Create(It.IsAny<Result>())).Returns(It.IsAny<Result>());

            //Act
            var controllerActionResult = await homeController.IndexAsync();

            //Assert
            Assert.NotNull(controllerActionResult);
            
        }

        [Fact]
        public void StoreEntryInDatabase_Test()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localhost:17829/days").Respond("text/plain", "6");
            mockHttp.When("http://localhost:44717/month").Respond("text/plain", "JAN");

            var client = new HttpClient(mockHttp);
            homeController = new HomeController(mockRepo.Object, client);

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
