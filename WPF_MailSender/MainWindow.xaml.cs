using System.Windows;
using System.Net;
using System.Windows.Media;
using System.Net.Mail;
using System;

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
            this.Title = "WpfMailSender";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(EmptyFields(String.IsNullOrEmpty(UserNameTextBox.Text), String.IsNullOrEmpty(PasswordBoxEditor.Password)))
            {
                EmailSendServiceClass ESSC = new EmailSendServiceClass(new NetworkCredential(UserNameTextBox.Text, PasswordBoxEditor.SecurePassword));

                ESSC.SendMessage(StaticVariables.MailSender, StaticVariables.MailReceiver, MessageBody.Text, MessageSubject.Text, this);
            }
        }

        /// <summary>
        /// Метод проверки пустых полей имени пользователя и пароля.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        private bool EmptyFields(bool User, bool Pass)
        {
            if(User | Pass)
            {
                string Title = "Input Error";
                string Text = "Fields Username and/or Password is empty. Enter your username and password";
                StaticVariables.GetNewMessageWindow(this, Title, Text, Brushes.OrangeRed).ShowDialog();
                return false;
            }
            return true;
        }

        private void WatermarkSubject_GotFocus(object sender, RoutedEventArgs e)
        {
            WatermarkSubject.Visibility = Visibility.Hidden;
            MessageSubject.Visibility = Visibility.Visible;
            MessageSubject.Focus();
        }

        private void MessageSubject_LostFocus(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(MessageSubject.Text))
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
    }
}
