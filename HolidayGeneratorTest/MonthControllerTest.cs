using MonthService.Controllers;
using System;
using Xunit;

namespace HolidayGeneratorTest
{
    public class MonthControllerTest
    {
        public MonthController monthContoller;

        [Fact]
        public void Get_Test()
        {
            //Arrange
            monthContoller = new MonthController();
            //Act
            var month = monthContoller.Get();

            //Assert
            Assert.True((month.GetType()).Equals(typeof(Months)));
           
        }
    }
}
