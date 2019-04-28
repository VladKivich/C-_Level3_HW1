using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MailSender.Interfaces;

namespace WPF_MailSender.Models
{
    public class EmailMessage
    {
        public string Subject { get; set; }

        public string Text { get; set; }

        public int _ID { get; private set; }

        public EmailMessage(string Subject, string Text, int ID = 0)
        {
            this.Subject = Subject;
            this.Text = Text;
            this.ID = ID;
        }

        public EmailMessage()
        {
            Subject = "Unknown";
            Text = "Unknown";
            ID = 0;
        }

        public int ID
        {
            get => _ID;
            set
            {
                _ID = value;
            }
        }
    }
}
