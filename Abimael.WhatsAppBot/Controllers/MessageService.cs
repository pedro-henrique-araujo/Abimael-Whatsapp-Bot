namespace Abimael.WhatsAppBot.Controllers
{
    public class MessageService
    {
        private IConfiguration _configuration;

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidToken(string token)
        {
            return _configuration["WhatsappToken"] == token;
        }

        public async Task HandleMessageNotificationAsync(MessageNotification notification)
        {
            var messages = notification.Entry.SelectMany(e => e.Changes.SelectMany(c => c.Value.Messages));
            foreach (var message in messages)
            {
                var messageToSend = new WhatsappMessage(message.From);
                await messageToSend.SendAsync("você disse: *" + message.Text.Body + "*");
            }
        }
    }
}
