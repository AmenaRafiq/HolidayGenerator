using DaysService.Controllers;
using System;
using System.Linq;
using Xunit;

namespace HolidayGeneratorTest
{
    public class DaysControllerTest
    {
        public DaysController daysContoller;

        [Fact]
        public void Get_Test()
        {
            //Arrange
            daysContoller = new DaysController();
            //Act
            var days = daysContoller.Get();

            //Assert
            Assert.IsType<int>(days);
            Assert.Contains(days, DaysController.Days);
        }
    }
}
