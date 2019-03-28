using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF_MailSender
{
    static class StaticVariables
    {
        public static readonly string Host = "smtp.yandex.ru";

        public static readonly string MessageSubject = "Some title";

        public static readonly string MessageBody = "Some text";

        public static string MailSender { get; set; } = "smasoda@yandex.ru"; //123456qwert

        public static string MailReceiver { get; set; } = "vladkivich@gmail.com";

        public static readonly int Port = 25;
        
        public static UserMessageWindow GetNewMessageWindow(Window Owner, string EmailTitle, string EmailText, SolidColorBrush Brush)
        {
            return UserMessageWindow.GetMessageWindow(Owner, EmailTitle, EmailText, Brush);
        }

    }
}
