using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaysService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class DaysController : ControllerBase
    {
        //array holding various lengths of a holiday
        public static readonly int[] Days = new[]
        {
            2,3,4,5,6,7,10,14,21,28,30
        };

        public DaysController() { }

        [HttpGet]
        public int Get()
        {
            //instantiate a random number generator
            var random = new Random();
            //get random integer between 0 and 10 as there are 11 values in the Days array
            var daysValue = random.Next(0, 10);
            //return the corresponding value in the array
            return Days[daysValue];
        }
    }
}
