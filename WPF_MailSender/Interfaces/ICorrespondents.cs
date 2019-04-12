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

        Recepient CurrentRecepient { get; set; }

        Sender CurrentSender { get; set; }

        ObservableCollection<Sender> Senders { get; }
    }
}
