using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthController : ControllerBase
    {
        private static readonly string[] Months = new[]
        {
            "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
        };

        [HttpGet]
        public string Get()
        {
            //instantiate a random number generator
            var random = new Random();
            //get random integer between 0 and 11 as there are 12 values in the Months array
            var monthValue = random.Next(0, 10);
            //return the corresponding value in the array
            return Months[monthValue];
        }
    }
}
