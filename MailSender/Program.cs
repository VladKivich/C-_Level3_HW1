using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace MailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 25);
            client.Credentials = new NetworkCredential("user_name", "password");
            client.EnableSsl = true;

            MailMessage message = new MailMessage("shmachilin@yandex.ru", "shmachilin@gmail.com");
            message.Subject = "Tecтовое письмо";
            message.Body = $"Текст тестового письма от {DateTime.Now.TimeOfDay}";
            client.Send(message);

        }
    }
}
