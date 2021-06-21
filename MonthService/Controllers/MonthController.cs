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

        public MonthController() { }

        [HttpGet]
        public Months Get()
        {
            //instantiate a random number generator
            var random = new Random();

            //convert to array
            var monthsArray = Enum.GetValues(typeof(Months));

            //get random month
            Months month = (Months)monthsArray.GetValue(random.Next(0, monthsArray.Length));

            return month;
        }
    }
}
