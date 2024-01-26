using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Abimael.WhatsAppBot.Function
{
    public class Function2
    {
        [FunctionName("Function2")]
        public void Run([QueueTrigger("myqueue-items")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
