using Abimael.WhatsappBot.Data;
using Abimael.WhatsappBot.Data.Entities;
using Abimael.WhatsAppBot.Data.Dto;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Abimael.WhatsAppBot.Function
{
    public class Function1
    {
        private readonly WhatsappDbContext _dbContext;

        public Function1(WhatsappDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {

            if (req.Method == "GET")
            {
                return HasValidToken(req);
            }
            try
            {
                using var reader = new StreamReader(req.Body);
                var notificationAsString = await reader.ReadToEndAsync();
                var notification = JsonSerializer.Deserialize<MessageNotificationDto>(notificationAsString,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });


                foreach (var entry in notification.Entry)
                {
                    foreach (var changes in entry.Changes)
                    {
                        if (changes.Value.Messages is null) continue;
                        foreach (var m in changes.Value.Messages)
                        {
                            var message = await CreateMessageAsync(m.From);
                            await QueueWhatsappMessageAsync(message);
                        }
                    }
                }                

                return new OkObjectResult("success");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private static IActionResult HasValidToken(HttpRequest req)
        {
            req.Query.TryGetValue("hub.verify_token", out var verifyToken);
            var token = Environment.GetEnvironmentVariable("WhatsappToken");
            if (verifyToken.FirstOrDefault() != token)
            {
                return new BadRequestResult();
            }
            req.Query.TryGetValue("hub.challenge", out var challenge);
            var result = challenge.FirstOrDefault();
            return new OkObjectResult(result);
        }

        private async Task QueueWhatsappMessageAsync(Message message)
        {
            var azureConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            QueueClient client = new(azureConnectionString, "myqueue-items");
            await client.CreateIfNotExistsAsync();
            await client.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(message.Id.ToString())));
        }

        private async Task<Message> CreateMessageAsync(string from)
        {
            var message = new Message { Id = Guid.NewGuid(), From = from };
            _dbContext.Entry(message).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return message;
        }
    }
}
