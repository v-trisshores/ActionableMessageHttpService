using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("TestFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log, 
            ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string value = req.Query["value"];
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //value = value ?? data?.value;

            // card1.
            string responseMessage = "{\"type\":\"AdaptiveCard\",\"version\":\"1.0\",\"body\":[{\"type\":\"TextBlock\",\"text\":\"DATA\"}],\"actions\":[{\"type\":\"Action.Http\",\"title\":\"Get data (no autoInvokeAction)\",\"method\":\"POST\",\"url\":\"https://funcapp-ac.azurewebsites.net/api/card1/\",\"body\":\"\"}]}";

            // cards 2 & 3.
            responseMessage = "{\"type\":\"AdaptiveCard\",\"version\":\"1.0\",\"body\":[{\"type\":\"TextBlock\",\"text\":\"DATA\"}],\"actions\":[{\"type\":\"Action.Http\",\"title\":\"Get data (autoInvokeAction)\",\"method\":\"POST\",\"url\":\"https://funcapp-ac.azurewebsites.net/api/card2/\",\"body\":\"\"}],\"autoInvokeAction\":{\"method\":\"POST\",\"url\":\"https://funcapp-ac.azurewebsites.net/api/card3/\",\"body\":\"\",\"type\":\"Action.Http\"}}";

            responseMessage = responseMessage.Replace("DATA", $"{context.FunctionName}: {DateTime.Now.ToLongTimeString()}");

            req.HttpContext.Response.Headers.Add("CARD-UPDATE-IN-BODY", "true");

            return new OkObjectResult(responseMessage);
        }
    }
}
