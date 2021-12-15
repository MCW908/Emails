using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Emails.Models.NewFolder
{
    public class EmailContext : DbContext
    {
        public DbSet<Email> Emails { get; set; }

        public EmailContext(DbContextOptions<EmailContext> options) : base(options) 
        { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Email>().HasData(
                new Email { EmailId = 1, Sender = "TestMail123@gmail.com", Recipient = "JoeBloggs@gmail.com", Subject = "Seed Email", Body = "Sample Text Sample Text" },
                new Email { EmailId = 2, Sender = "JoeBloggs@gmail.com", Recipient = "AABB@gmail.com", Subject = "Testing", Body = "Seed Data Seed Data Seed Data" },
                new Email { EmailId = 3, Sender = "AABB@gmail.com", Recipient = "TestMail123@gmail.com", CC = "JoeBloggs@gmail.com", Subject = "CC Email", Body = "CC Email Test CC Email Test" }
            );
        }

    }
}
