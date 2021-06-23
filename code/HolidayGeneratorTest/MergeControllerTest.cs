using MergeService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonthService.Controllers;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HolidayGeneratorTest
{
    public class MergeControllerTest
    {
        private MergeController mergeController;
        private Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

        [Fact]
        public async Task Get_TestAsync()
        {
            //Arrange 
            Environment.SetEnvironmentVariable("daysServiceURL", "http://localhost:17829");
            Environment.SetEnvironmentVariable("monthServiceURL", "http://localhost:44717");
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://localhost:17829/days").Respond("text/plain", "6");
            mockHttp.When("http://localhost:44717/month").Respond("text/plain", "JAN");
            
            var client = new HttpClient(mockHttp);
            mergeController = new MergeController(client);

            //Act
            var controllerActionResult = await mergeController.Get();
            var result = (string)((OkObjectResult)controllerActionResult).Value;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.NotNull(controllerActionResult);
            Assert.IsType<int>(mergeController.daysServiceResponse);
            Assert.IsType<Month>(mergeController.monthServiceResponse);
        }

        
        [Fact]
        public void GetResultAllMonths_Test()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();

            //test every month
            foreach(Months m in Enum.GetValues(typeof(Months)))
            {
                //Arrange
                mockHttp.When("http://localhost:17829/days").Respond("text/plain", "6");
                mockHttp.When("http://localhost:44717/month").Respond("text/plain", m.ToString());
                var client = new HttpClient(mockHttp);
                mergeController = new MergeController(client);
                Month testMonth = (Month)m;

                //Act
                var result = mergeController.GetResult(2, testMonth);

                //Assert
                Assert.NotNull(result);
                Assert.IsType<string>(result);
            }

        }

        
        [Fact]
        public void GetHotCountry_Test()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:17829/days").Respond("text/plain", "6");
            mockHttp.When("http://localhost:44717/month").Respond("text/plain", "JAN");
            var client = new HttpClient(mockHttp);
            mergeController = new MergeController(client);

            //Act
            var country = mergeController.GetHotCountry();

            //Assert
            Assert.NotNull(country);
            Assert.IsType<string>(country);
            Assert.Contains(country, MergeController.HotCountries);

        }

        [Fact]
        public void GetColdCountry_Test()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:17829/days").Respond("text/plain", "6");
            mockHttp.When("http://localhost:44717/month").Respond("text/plain", "JUN");
            var client = new HttpClient(mockHttp);
            mergeController = new MergeController(client);

            //Act
            var country = mergeController.GetColdCountry();

            //Assert
            Assert.NotNull(country);
            Assert.IsType<string>(country);
            Assert.Contains(country, MergeController.ColdCountries);

        }
    }
}
