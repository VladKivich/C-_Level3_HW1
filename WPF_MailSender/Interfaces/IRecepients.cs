using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MailSender.Interfaces
{
    public interface IRecepients
    {
        IEnumerable<Recepient> GetAllRecepients();

        Recepient GotRecepientById(int id);

        void Edit(Recepient recepient);
    }
}
