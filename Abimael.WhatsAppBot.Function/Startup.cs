using Abimael.WhatsappBot.Data;
using Abimael.WhatsAppBot.Function;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup((typeof(Startup)))]
namespace Abimael.WhatsAppBot.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<WhatsappDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("WhatsappDatabase"));
            });
        }
    }
}