using System.Windows;
using System.Net;
using System.Windows.Media;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Security;

namespace WPF_MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //TODO: Перенести логику в модель представление
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SendersUser.HasItems & RecepientsUser.HasItems)
            {
                if(EmptyFields(String.IsNullOrEmpty(MessageBody.Text), String.IsNullOrEmpty(MessageSubject.Text)))
                {
                    foreach (Sender S in SendersUser.Items)
                    {
                        foreach (Recepient R in RecepientsUser.Items)
                        {
                            EmailSendServiceClass ESSC = new EmailSendServiceClass(S.ID, S.Server, S.Port);

                            ESSC.SendMessage(S.Email, R.Email, MessageBody.Text, MessageSubject.Text, this);
                        }
                    }
                }
            }
            else
            {
                string Title = "No Sender/Recepient";
                string Text = "Select sender and / or recipient";
                StaticVariables.GetNewMessageWindow(this, Title, Text, Brushes.OrangeRed).ShowDialog();
            }
        }
        
        private bool EmptyFields(bool Subject, bool Message)
        {
            if(Subject | Message)
            {
                string Title = "Input Error";
                string Text = "Your message is empty!";
                StaticVariables.GetNewMessageWindow(this, Title, Text, Brushes.OrangeRed).ShowDialog();
                return false;
            }
            return true;
        }
        
    }
}
