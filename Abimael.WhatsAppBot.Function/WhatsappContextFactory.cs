using Abimael.WhatsappBot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Abimael.WhatsAppBot.Function
{
    public class WhatsappContextFactory : IDesignTimeDbContextFactory<WhatsappDbContext>
    {
        public WhatsappDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WhatsappDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("WhatsappDatabase");
            optionsBuilder.UseSqlServer(connectionString);
            return new WhatsappDbContext(optionsBuilder.Options);
        }
    }
}
