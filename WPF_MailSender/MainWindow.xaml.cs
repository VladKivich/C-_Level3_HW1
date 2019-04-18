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

        #region Watermarks

        private void WatermarkSubject_GotFocus(object sender, RoutedEventArgs e)
        {
            WatermarkSubject.Visibility = Visibility.Hidden;
            MessageSubject.Visibility = Visibility.Visible;
            MessageSubject.Focus();
        }

        private void MessageSubject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(MessageSubject.Text))
            {
                MessageSubject.Visibility = Visibility.Collapsed;
                WatermarkSubject.Visibility = Visibility.Visible;
            }
        }

        private void WatermarkBody_GotFocus(object sender, RoutedEventArgs e)
        {
            WatermarkBody.Visibility = Visibility.Hidden;
            MessageBody.Visibility = Visibility.Visible;
            MessageBody.Focus();
        }

        private void MessageBody_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(MessageBody.Text))
            {
                MessageBody.Visibility = Visibility.Collapsed;
                WatermarkBody.Visibility = Visibility.Visible;
            }
        }


        #endregion
    }
}
