﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MailSender.Interfaces;

namespace WPF_MailSender.ViewModel
{
    public enum EditorWindowMode
    {
        Sender,
        Recepient
    }

    public class EditorWindowViewModel: ViewModelBase, IDataErrorInfo
    {
        #region Заголовок окна редактора

        public string _Title = "";

        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }

        #endregion

        #region Кнопка действия в окне редактора

        public string _ActionButtonContent = "";

        public string ActionButtonContent
        {
            get { return _ActionButtonContent; }
            set { Set(ref _ActionButtonContent, value); }
        }

        #endregion

        #region Команды

        public ICommand SaveChanges { get; }

        public ICommand Exit { get; }

        #endregion

        #region Поля\Свойства

        string IDataErrorInfo.Error => "";

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    case nameof(EmailAddress):
                        if (!EmailAddress.Contains("@") | EmailAddress.Length < 4) return "Неверно указан адрес электронной почты";
                        break;

                    case "SMTP":
                        if (!SMTP.Contains("@") & !SMTP.Contains(".")) return "Неверно указан адрес сервера";
                        if(SMTP.Length < 4) return "Неверно указан адрес сервера";
                        break;

                    case "Name":
                        if (Name is null) return "Введите имя";
                        break;

                    case "Port":
                        if (Port <= 0) return "Неверно указан адрес порта";
                        break;
                }
                return "";
            }
        }
        
        private string _Name;

        public virtual string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        private string _EmailAddress;

        public virtual string EmailAddress
        {
            get => _EmailAddress;
            set => Set(ref _EmailAddress, value);
        }

        private string _SMTP;

        public virtual string SMTP
        {
            get => _SMTP;
            set => Set(ref _SMTP, value);
        }

        private int _Port;

        public virtual int Port
        {
            get => _Port;
            set => Set(ref _Port, value);
        }

        private string _Password;

        public virtual string Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }

        #endregion
        
        #region События\Делегаты

        public event EventDelegate<bool> Closed;

        public delegate void EventDelegate<T>(object sender, T arg);

        #endregion
        
        public bool AllFields { get; private set; }

        private readonly EditorWindowMode Mode;

        public EditorWindowViewModel(string Title, string ActionButton, EditorWindowMode Mode)
        {
            _Title = Title;

            _ActionButtonContent = ActionButton;

            this.Mode = Mode;

            AllFields = (this.Mode == EditorWindowMode.Recepient ? false : true );

            SaveChanges = new RelayCommand(ChangeButton);

            Exit = new RelayCommand(ExitButton);

            #region Заглушка

            Sender S = new Sender();
            Name = S.Name;
            EmailAddress = S.Email;
            SMTP = S.Server;
            Port = S.Port;
            Password = S.ID.Password;

            #endregion

        }

        public void GotRecepient(Recepient R)
        {
            if(R is null)
            {
                R = new Recepient();
            }

            EmailAddress = R.Email;
        }

        public void GotSender(Sender S)
        {
            
        }

        private void ChangeButton()
        {
            Closed?.Invoke(this, true);
        }

        private void ExitButton()
        {
            Closed?.Invoke(this, false);
        }
    }
}
