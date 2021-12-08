using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Emails.Models
{
    public class Email
    {

        public Email()
        { 
            
        }

        public Email(string sender, string recipient, string cc, string subject, string body) 
        {
            Sender = sender;
            Recipient = recipient;
            CC = cc;
            Subject = subject;
            Body = body;
        }

        [Required]
        public int EmailId { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Recipient { get; set; }

        public string CC { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
