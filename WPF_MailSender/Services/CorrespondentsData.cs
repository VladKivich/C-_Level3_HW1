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
    public class CorrespondentsData : ICorrespondents
    {
        //public Recepient _CurrentRecepient;

        //public Sender _CurrentSender;

        private MailSenderDBDataContext Context;
        
        public ObservableCollection<Sender> Senders { get; private set; }

        public CorrespondentsData(MailSenderDBDataContext Context)
        {
            //Получаем контекст БД получателей
            this.Context = Context;

            //Заполняем список отправителей
            Senders  = new ObservableCollection<Sender>()
            {
                new Sender("A", "smasoda@yandex.ru", "smtp.yandex.ru", 25, new NetworkCredential("smasoda@yandex.ru", "123456qwert"), SenderNumber()),
                new Sender("B", "smasoda@yandex.ru", "smtp.yandex.ru", 55, new NetworkCredential("smasoda@yandex.ru", "123456qwert"), SenderNumber())
            };
        }

        private int SenderNumber()
        {
            if(Senders is null)
            {
                return 1;
            }
            else
            {
                int numb = Senders.Count + 1;
                return numb;
            }
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
        public void AddNewRecepient(string email)
        {
            Data.Recepient R = new Data.Recepient
            {
                Email = email
            };

            Context.Recepient.InsertOnSubmit(R);

            Context.SubmitChanges();
        }

        /// <summary>
        /// Удаляет выбранного получателя.
        /// </summary>
        /// <param name="recepient">Удаляемый получатель</param>
        public void Delete(Recepient recepient)
        {
            var db_recepient = Context.Recepient.FirstOrDefault(r => r.Id == recepient.ID);
            if (db_recepient == null) return;

            Context.Recepient.DeleteOnSubmit(db_recepient);

            Context.SubmitChanges();
        }

        /// <summary>
        /// Удаляет выбранного отправителя.
        /// </summary>
        /// <param name="sender">Отправитель для удаления</param>
        public void Delete(Sender sender)
        {
            var _sender = Senders.FirstOrDefault(s => s.Number == sender.Number);
            if (_sender == null) return;

            Senders.Remove(sender);
        }


        /// <summary>
        /// Добавляет нового отправителя в коллекцию
        /// </summary>
        /// <param name="sender">Отправитель для добавления</param>
        public void AddNewSender(Sender sender)
        {
            sender.Number = SenderNumber();
            Senders.Add(sender);
        }

        /// <summary>
        /// Редактирует выбранного отправителя.
        /// </summary>
        /// <param name="sender">Отправитель для редактирования</param>
        public void Edit(Sender sender)
        {
            Sender _sender = Senders.FirstOrDefault(s => s.Number == sender.Number);
            if (_sender == null) return;

            _sender = sender;
        }
        
    }
}
