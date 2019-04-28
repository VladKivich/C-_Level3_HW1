using System.ComponentModel;
using System.Net;

namespace WPF_MailSender
{
    public class Sender 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public int Number { get; set; }

        public NetworkCredential ID { get; private set; }
        

        public Sender(string Name, string Email, string Server, int Port, NetworkCredential ID, int Number = 0)
        {
            this.Name = Name;
            this.Email = Email;
            this.Server = Server;
            this.Port = Port;
            this.ID = ID;
            this.Number = Number;
        }

        public Sender()
        {
            Name = "Unknown";
            Email = "Unknown";
            Server = "Unknown";
            Port = 0;
            ID = new NetworkCredential(Email, "password");
            Number = 0;
        }
    }
}
