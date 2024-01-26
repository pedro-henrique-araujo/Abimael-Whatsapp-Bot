using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Abimael.WhatsAppBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WhatsappController : ControllerBase
    {
        private readonly MessageService _messageService;

        public WhatsappController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery(Name = "hub.challenge")] string hubChallenge, 
            [FromQuery(Name = "hub.verify_token")] string token)
        {
           if (_messageService.IsValidToken(token) == false)
            {
                return BadRequest();
            }
            return Ok(hubChallenge);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MessageNotification notification)
        {
            await _messageService.HandleMessageNotificationAsync(notification);
            return Ok();
        }
    }
}