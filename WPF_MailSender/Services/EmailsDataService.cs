using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MailSender.Interfaces;
using WPF_MailSender.Models;

namespace WPF_MailSender.Services
{
    public class EmailsDataService : IMessages
    {
        public List<EmailMessage> EmailsList { get; private set; }
        
        public EmailsDataService()
        {
            EmailsList = new List<EmailMessage>();
            Add(new EmailMessage("Congratulations!", "You have received an email."));
        }

        public List<EmailMessage> GetAll() => EmailsList;

        public void Add(EmailMessage EMessage)
        {
            if (GetById(EMessage.ID) != null) return;
            else
            {
                EMessage.ID = EmailsList.Count + 1;
                EmailsList.Add(EMessage);
            }
        }

        public void Edit(EmailMessage EMessage)
        {
            EmailMessage Edit = GetById(EMessage.ID);
            if (Edit == null) return;
            else
            {
                Edit.Subject = EMessage.Subject;
                Edit.Text = EMessage.Text;
            }
        }

        public EmailMessage GetById(int Id)
        {
            return EmailsList.Find(e => e.ID == Id);
        }

        public void Remove(int Id)
        {
            EmailsList.Remove(GetById(Id));
        }
    }
}
