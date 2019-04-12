using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MailSender.Interfaces;
using WPF_MailSender.Data;
using System.Collections.ObjectModel;
using System.Net;

namespace WPF_MailSender.Services
{
    public class CorrespondentsData : IRecepients
    {
        public Recepient _CurrentRecepient;

        public Sender _CurrentSender;

        private MailSenderDBDataContext Context;
        
        public ObservableCollection<Sender> Senders { get; }

        public CorrespondentsData(MailSenderDBDataContext Context)
        {
            //Получаем контекст БД получателей
            this.Context = Context;

            //Заполняем список отправителей
            Senders  = new ObservableCollection<Sender>()
            {
                new Sender("A", "smasoda@yandex.ru", "smtp.yandex.ru", 25, new NetworkCredential("smasoda@yandex.ru", "123456qwert"))
            };
        }

        /// <summary>
        /// Возвращает коллекцию получателей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Recepient> GetAllRecepients()
        {
            var Recepients = from r in Context.Recepient
                             orderby r.Id
                             select r;

            return Recepients.Select(r => new Recepient(r.Id, r.Email)).ToList();
        }

        /// <summary>
        /// Возвращает получателя по его ID
        /// </summary>
        /// <param name="id">ID получателя</param>
        /// <returns></returns>
        public Recepient GotRecepientById(int id)
        {
            var Recepient = Context.Recepient.FirstOrDefault(r => r.Id == id);
            if(Recepient != null)
            {
                return new Recepient(Recepient.Id, Recepient.Email);
            }
            return new Recepient();
        }

        /// <summary>
        /// Редактирует получателя.
        /// </summary>
        /// <param name="recepient">Редактируемый получатель</param>
        public void Edit(Recepient recepient)
        {
            var db_recepient = Context.Recepient.FirstOrDefault(r => r.Id == recepient.ID);
            if (db_recepient == null) return;

            db_recepient.Email = recepient.Email;

            Context.SubmitChanges();
        }

        /// <summary>
        /// Добавляем нового получателя в БД
        /// </summary>
        /// <param name="email">Адрес электронной почты получателя</param>
        public void AddNew(string email)
        {
            Data.Recepient R = new Data.Recepient
            {
                Email = email
            };

            Context.Recepient.InsertOnSubmit(R);

            Context.SubmitChanges();
        }

        public Recepient CurrentRecepient
        {
            get
            {
                return _CurrentRecepient;
            }
            set
            {
                _CurrentRecepient = value;
            }
        }

        public Sender CurrentSender
        {
            get
            {
                return _CurrentSender;
            }
            set
            {
                _CurrentSender = value;
            }
        }
        
    }
}
