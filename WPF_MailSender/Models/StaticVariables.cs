using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static WPF_MailSender.EditorWindow;

namespace WPF_MailSender
{
    public class Sender
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }

        public NetworkCredential ID { get; private set; }

        public Sender(string Name, string Email, string Server, int Port, NetworkCredential ID)
        {
            this.Name = Name;
            this.Email = Email;
            this.Server = Server;
            this.Port = Port;
            this.ID = ID;
        }

        public Sender()
        {
            Name = "Unknown";
            Email = "Unknown";
            Server = "Unknown";
            this.Port = 0;
            this.ID = new NetworkCredential(Email, "password");
        }
    }

    public class Recepient
    {
        public int ID { get; private set; }

        public string Email { get; set; }

        public Recepient(int ID, string Email)
        {
            this.ID = ID;
            this.Email = Email;
        }

        public Recepient()
        {
            ID = 0;
            Email = "Unknown";
        }

        public Recepient(string Email)
        {
            this.Email = Email;
        }

        public override string ToString()
        {
            return String.Format($"{ID} : {Email.ToString()}");
        }

        public Recepient SetParameters(string Email)
        {
            return new Recepient(Email);
        }
    }

    static class StaticVariables
    {
        public static UserMessageWindow GetNewMessageWindow(Window Owner, string EmailTitle, string EmailText, SolidColorBrush Brush, Visibility Exit = Visibility.Visible)
        {
            return UserMessageWindow.GetMessageWindow(Owner, EmailTitle, EmailText, Brush, Exit);
        }
    }
}
