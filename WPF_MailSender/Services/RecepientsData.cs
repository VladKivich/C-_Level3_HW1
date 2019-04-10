using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MailSender.Interfaces;
using WPF_MailSender.Data;

namespace WPF_MailSender.Services
{
    public class RecepientsData : IRecepients
    {
        private MailSenderDBDataContext Context;

        public RecepientsData(MailSenderDBDataContext Context)
        {
            this.Context = Context;
        }
        
        public IEnumerable<Recepient> GetAllRecepients()
        {
            var Recepients = from r in Context.Recepient
                             orderby r.Id
                             select r;

            return Recepients.Select(r => new Recepient(r.Id, r.Email)).ToList();
        }

        public Recepient GotRecepientById(int id)
        {
            var Recepient = Context.Recepient.FirstOrDefault(r => r.Id == id);
            if(Recepient != null)
            {
                return new Recepient(Recepient.Id, Recepient.Email);
            }
            return new Recepient();
        }

        public void Edit(Recepient recepient)
        {
            var db_recepient = Context.Recepient.FirstOrDefault(r => r.Id == recepient.ID);
            if (db_recepient == null) return;

            db_recepient.Email = recepient.Email;

            Context.SubmitChanges();
        }

        public void AddNew(string email)
        {
            Data.Recepient R = new Data.Recepient
            {
                Email = email
            };
            Context.Recepient.InsertOnSubmit(R);
            Context.SubmitChanges();
        }
    }
}
