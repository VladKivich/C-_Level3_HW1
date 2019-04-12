using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MailSender.Interfaces
{
    public interface ICorrespondents
    {
        IEnumerable<Recepient> GetAllRecepients();

        Recepient GotRecepientById(int id);

        void Edit(Recepient recepient);

        void AddNew(string email);

        void Delete(Recepient recepient);

        ObservableCollection<Sender> Senders { get; }
    }
}
