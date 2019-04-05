using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF_MailSender
{
    public interface IEmail
    {
        string Email { get; set; }

        Recepient SetParameters(string Email);
    }

    public interface ISender
    {
        string Name { get; set; }
        string Server { get; set; }
        int Port { get; set; }
        SecureString Password { set; }

        Sender SetParameters(string Name, string Email, string Server, int Port, SecureString Password);
    }

    public class Sender: IEmail, ISender
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public SecureString Password { get; set; }

        public Sender(string Name, string Email, string Server, int Port, SecureString Password)
        {
            this.Name = Name;
            this.Email = Email;
            this.Server = Server;
            this.Port = Port;
            this.Password = Password;
        }

        public Sender SetParameters(string Name, string Email, string Server, int Port, SecureString Password)
        {
            return new Sender(Name, Email, Server, Port, Password);
        }

        public Recepient SetParameters(string Email)
        {
            return new Recepient(Email);
        }
    }

    public class Recepient: IEmail
    {
        public string Email { get; set; }
        
        public Recepient(string Email)
        {
            this.Email = Email;
        }

        public override string ToString()
        {
            return String.Format(Email.ToString());
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

        public static EditorWindow GetNewEditorWindow(Window Owner, IList List, Type T, string Title)
        {
            return EditorWindow.GetEditorWindow(Owner, List, T, Title);
        }

        public static EditorWindow GetNewEditorWindow(Window Owner, IList List, IEmail New, string Title)
        {
            return EditorWindow.GetEditorWindow(Owner, List, New, Title);
        }
    }
}
