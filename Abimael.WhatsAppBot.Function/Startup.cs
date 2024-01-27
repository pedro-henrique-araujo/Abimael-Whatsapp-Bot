using Abimael.WhatsappBot.Data;
using Abimael.WhatsAppBot.Function;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup((typeof(Startup)))]
namespace Abimael.WhatsAppBot.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<WhatsappDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Whatsapp;Trusted_Connection=True");
            });
        }
    }
}