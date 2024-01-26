using System.Net.Http.Headers;

namespace Abimael.WhatsAppBot.Controllers
{
    public class WhatsappMessage
    {
        private readonly string _to;

        public WhatsappMessage(string to)
        {
            _to = to;
        }
        
        public async Task SendAsync(string body)
        {
            var baseUrl = "{message_url}";
            var httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "{token}");
                await httpClient.PostAsJsonAsync(baseUrl, new
                {
                    messaging_product = "whatsapp",
                    to = _to,
                    text = new MessageText { Body = body  }
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
