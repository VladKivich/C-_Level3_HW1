using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media;

namespace WPF_MailSender
{
    public class EmailSendServiceClass
    {
        public SmtpClient SMTP { get; private set; }

        public MailMessage Message { get; private set; }
        
        public EmailSendServiceClass(NetworkCredential ID)
        {
            SMTP = new SmtpClient(StaticVariables.Host, StaticVariables.Port);
            SMTP.Credentials = ID;
            SMTP.EnableSsl = true;
        }

        public void SendMessage(string From, string To, string MessageText, string MessageSubject, Window MainWindow)
        {
            Message = new MailMessage(From, To, MessageSubject, MessageText);

            try
            {
                SMTP.Send(Message);
                StaticVariables.GetNewMessageWindow(MainWindow, "Success", "Your email has been sent!", Brushes.GreenYellow).ShowDialog();
            }

            catch (Exception E)
            {
                WindowCollection WC = MainWindow.OwnedWindows;
                StaticVariables.GetNewMessageWindow(MainWindow, "Sending Error", E.Message, Brushes.DarkRed).ShowDialog();
            }
        }
    }
}
