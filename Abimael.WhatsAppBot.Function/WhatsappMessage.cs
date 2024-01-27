using Abimael.WhatsAppBot.Data.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Abimael.WhatsAppBot.Function
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
            var baseUrl = Environment.GetEnvironmentVariable("WhatsappSendMessageUrl");
            var authToken = Environment.GetEnvironmentVariable("WhatsappSendToken");
            var httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                var payload = new
                {
                    messaging_product = "whatsapp",
                    to = _to,
                    type = "text",
                    text = new { body }
                };
                var response = await httpClient.PostAsJsonAsync(baseUrl, payload);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
