using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MailSender.Models;

namespace WPF_MailSender.Interfaces
{
    public interface IMessages
    {
        List<EmailMessage> GetAll();

        EmailMessage GetById(int Id);

        void Add(EmailMessage EMessage);

        void Remove(int Id);

        void Edit(EmailMessage EMessage);
    }
}
