using Abimael.WhatsappBot.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abimael.WhatsappBot.Data
{
    public class WhatsappDbContext : DbContext
    {
        public WhatsappDbContext(DbContextOptions<WhatsappDbContext> options) :base(options)
        {            
        }

        public DbSet<Message> Messages { get; set; }
    }
}