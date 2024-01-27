using Abimael.WhatsappBot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Abimael.WhatsAppBot.Function
{
    public class WhatsappContextFactory : IDesignTimeDbContextFactory<WhatsappDbContext>
    {
        public WhatsappDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
               .AddJsonFile("appsettings.json", optional: false)
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<WhatsappDbContext>();
            var connectionString = configuration.GetConnectionString("WhatsappDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            return new WhatsappDbContext(optionsBuilder.Options);
        }
    }
}
