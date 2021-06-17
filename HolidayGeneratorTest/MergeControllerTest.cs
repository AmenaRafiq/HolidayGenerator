using MergeService.Controllers;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace HolidayGeneratorTest
{
    public class MergeControllerTest
    {
        private MergeController mergeController;
        private Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();


        [Fact]
        public void GetHotCountry_Test()
        {
            //Arrange
            mergeController = new MergeController(mockConfiguration.Object);
            //Act
            var country = mergeController.GetHotCountry();

            //Assert
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
            Assert.IsType<string>(country);
            Assert.Contains(country, MergeController.ColdCountries);

        }
    }
}
