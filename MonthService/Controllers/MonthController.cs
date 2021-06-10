using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class MonthController : ControllerBase
    {

        [HttpGet]
        public Months Get()
        {
            //instantiate a random number generator
            var random = new Random();

            var monthsArray = Enum.GetValues(typeof(Months));
            var len = random.Next(0, monthsArray.Length);

            Months month = (Months)monthsArray.GetValue(random.Next(0, len));

            return month;
        }
    }
}
