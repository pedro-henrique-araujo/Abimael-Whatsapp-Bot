using Abimael.WhatsappBot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Abimael.WhatsAppBot.Function
{
    public class WhatsappContextFactory : IDesignTimeDbContextFactory<WhatsappDbContext>
    {
        public WhatsappDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WhatsappDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Whatsapp;Trusted_Connection=True");
            return new WhatsappDbContext(optionsBuilder.Options);
        }
    }
}
