using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace MonthFunction
{
    public static class MonthFunc
    {
        [Function("MonthFunc")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("MonthFunc");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //instantiate a random number generator
            var random = new Random();
            //convert to array
            var monthsArray = Enum.GetValues(typeof(Months));
            //get random month
            Months month = (Months)monthsArray.GetValue(random.Next(0, monthsArray.Length));

            response.WriteString(month.ToString());

            return response;
        }
    }
}
