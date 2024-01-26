using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Abimael.WhatsAppBot.Function
{
    public static class Function1
    {

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var azureConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                QueueClient client = new(azureConnectionString, "myqueue-items");
                await client.CreateIfNotExistsAsync();
                await client.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes("Demo")));

                return new OkObjectResult("success");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
