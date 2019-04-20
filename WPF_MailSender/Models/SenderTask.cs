using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MailSender.Models
{
    class SenderTask
    {
        public Sender Sender { get; set; }
        public List<Recepient> RecepientsList { get; private set; }
        public EmailMessage Message { get; set; }
        public TaskTime Time { get; set; }

        public SenderTask(Sender Sender, params Recepient[] Recepients )
        {
            this.Sender = Sender;
            RecepientsList = new List<Recepient>();
            RecepientsList.AddRange(Recepients);
        }
    }
}
