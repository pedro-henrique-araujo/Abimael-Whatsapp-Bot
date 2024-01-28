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
            var text = @"
Bem-vindo à Abimael Ltda.! 😀

Explore nossos produtos digitais e descubra um mundo de possibilidades
Estamos aqui para ajudá-lo(a) a alcançar seus objetivos e superar suas expectativas.

Veja abaixo alguns dos nosso produtos

- *Curso Online ""Desenvolvimento de Aplicativos para Iniciantes"":* https://exemplo.com
- *E-book ""Guia Completo para Marketing Digital"":* https://exemplo.com
- *Programa de Mentoria ""Cresça sua Startup"":* https://exemplo.com

Que sua jornada conosco seja repleta de sucesso e realizações!

";
            await whatsappMessage.SendAsync(text);
        }
    }
}
