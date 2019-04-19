using System;
using System.ComponentModel;

namespace WPF_MailSender
{
    public class Recepient:IDataErrorInfo
    {
        public int ID { get; private set; }

        public string Email { get; set; }

        string IDataErrorInfo.Error => "";

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    case nameof(Email):
                        if (!Email.Contains("@")) return "Неверно указан адрес электронной почты";
                        break;
                }

                return "";
            }
        }

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
}
