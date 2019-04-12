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

        void AddNewRecepient(string email);
        
        void Delete(Recepient recepient);

        ObservableCollection<Sender> Senders { get; }

        void AddNewSender(Sender sender);

        void Edit(Sender sender);

        void Delete(Sender sender);
    }
}
