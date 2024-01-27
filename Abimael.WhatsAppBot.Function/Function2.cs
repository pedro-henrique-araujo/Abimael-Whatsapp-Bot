using System;
using System.Threading.Tasks;
using Abimael.WhatsappBot.Data;
using Abimael.WhatsappBot.Data.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Abimael.WhatsAppBot.Function
{
    public class Function2
    {
        private WhatsappDbContext _dbContext;

        public Function2(WhatsappDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [FunctionName("Function2")]
        public async Task Run([QueueTrigger("myqueue-items")] string myQueueItem, ILogger log)
        {
            var messagedReceived = await _dbContext.Set<Message>().FirstOrDefaultAsync(m => m.Id == new Guid(myQueueItem));
            var whatsappMessage = new WhatsappMessage(messagedReceived.From);
            await whatsappMessage.SendAsync("my little message");
        }
    }
}
