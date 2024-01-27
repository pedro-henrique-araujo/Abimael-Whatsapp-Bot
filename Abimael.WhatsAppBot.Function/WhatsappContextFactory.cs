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
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("WhatsappDatabase"));
            var dbContext = new WhatsappDbContext(optionsBuilder.Options);
            dbContext.Database.Migrate();
            return dbContext;
        }
    }
}
