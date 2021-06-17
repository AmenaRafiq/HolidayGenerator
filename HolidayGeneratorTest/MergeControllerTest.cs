using MergeService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonthService.Controllers;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HolidayGeneratorTest
{
    public class MergeControllerTest
    {
        private MergeController mergeController;
        private Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();

        [Fact]
        public void Get_Test()
        {
            //Arrange 
            mergeController = new MergeController(mockConfiguration.Object);

            //Act
            var controllerActionResult = mergeController.Get();

            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<Task<IActionResult>>(controllerActionResult);
        }

        [Fact]
        public void GetResult_Test()
        {
            //Arrange
            mergeController = new MergeController(mockConfiguration.Object);
            Months testMonth = Months.JAN;

            //Act
            var result = mergeController.GetResult(2, testMonth);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            

        }

        [Fact]
        public void GetHotCountry_Test()
        {
            //Arrange
            mergeController = new MergeController(mockConfiguration.Object);

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
            mergeController = new MergeController(mockConfiguration.Object);

            //Act
            var country = mergeController.GetColdCountry();

            //Assert
            Assert.NotNull(country);
            Assert.IsType<string>(country);
            Assert.Contains(country, MergeController.ColdCountries);

        }
    }
}
