using System.Windows;
using System.Net;
using System.Windows.Media;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

namespace WPF_MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Recepient> RecepientsList;

        public ObservableCollection<Sender> SendersListBase;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "WPF Mail Sender";

            SendersListBase = new ObservableCollection<Sender>
            {
                new Sender("A", StaticVariables.MailSender, "smtp.yandex.ru", 25),
                new Sender("B", StaticVariables.MailSender, "smtp.yandex.ru", 25),
                new Sender("C", StaticVariables.MailSender, "smtp.yandex.ru", 25),
            };

            RecepientsList = new ObservableCollection<Recepient>
            {
                new Recepient("vladkivich@gmail.com")
            };

            SendersBase.ItemsSource = SendersListBase;
            RecepientsBase.ItemsSource = RecepientsList;
        }

        public void CollectionUpdate(IList Collect)
        {
            SendersBase.ItemsSource = Collect;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (EmptyFields(String.IsNullOrEmpty(UserNameTextBox.Text), String.IsNullOrEmpty(PasswordBoxEditor.Password)))
            //{
            //    EmailSendServiceClass ESSC = new EmailSendServiceClass(new NetworkCredential(UserNameTextBox.Text, PasswordBoxEditor.SecurePassword));

            //    ESSC.SendMessage(StaticVariables.MailSender, StaticVariables.MailReceiver, MessageBody.Text, MessageSubject.Text, this);
            //}
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

        #region Add\Remove Senders

        private void SendersAdd_Click(object sender, RoutedEventArgs e)
        {
            if (SendersBase.SelectedItems.Count == 0) return;
            foreach (var item in SendersBase.SelectedItems)
            {
                if (!SendersUser.Items.Contains(item))
                {
                    SendersUser.Items.Add(item);
                }
            }
        }

        private void SendersRemove_Click(object sender, RoutedEventArgs e)
        {
            if (SendersUser.SelectedItems.Count == 0) return;
            for (int i = SendersUser.SelectedItems.Count - 1; i > -1; i--)
            {
                SendersUser.Items.Remove(SendersUser.SelectedItems[i]);
            }
        }

        #endregion

        #region Add\Remove Recepients

        private void RecipientAdd_Click(object sender, RoutedEventArgs e)
        {
            if (RecepientsBase.SelectedItems.Count == 0) return;
            foreach (var item in RecepientsBase.SelectedItems)
            {
                if (!RecepientsUser.Items.Contains(item))
                {
                    RecepientsUser.Items.Add(item);
                }
            }
        }

        private void RecipientRemove_Click(object sender, RoutedEventArgs e)
        {
            if (RecepientsUser.SelectedItems.Count == 0) return;
            for (int i = RecepientsUser.SelectedItems.Count-1; i > -1; i--)
            {
                RecepientsUser.Items.Remove(RecepientsUser.SelectedItems[i]);
            }
        }

        #endregion

        #region New Sender\Recepient

        private void NewSender_Click(object sender, RoutedEventArgs e)
        {
            StaticVariables.GetNewEditorWindow(this, SendersListBase, typeof(Sender), "Create New Sender");
        }

        private void NewRecepient_Click(object sender, RoutedEventArgs e)
        {
            StaticVariables.GetNewEditorWindow(this, RecepientsList, typeof(Recepient), "Create New Recepient");
        }

        #endregion
        
        #region Edit Selected Sender\Recepient

        private void EditSender_Click(object sender, RoutedEventArgs e)
        {
            if (SendersBase.SelectedItems.Count == 0)
            {
                StaticVariables.GetNewMessageWindow(this, "Select Error", "Please select Sender from Sender Selection List", Brushes.OrangeRed, Visibility.Visible).ShowDialog();
                return;
            }
            StaticVariables.GetNewEditorWindow(this, SendersListBase, SendersBase.SelectedItem as IEmail, "Edit Sender");
        }

        private void EditRecepient_Click(object sender, RoutedEventArgs e)
        {
            if (RecepientsBase.SelectedItems.Count == 0)
            {
                StaticVariables.GetNewMessageWindow(this, "Select Error", "Please select Recepient from Recepient Selection List", Brushes.OrangeRed, Visibility.Visible).ShowDialog();
                return;
            }
            StaticVariables.GetNewEditorWindow(this, RecepientsList, RecepientsBase.SelectedItem as IEmail, "Edit Sender");
        }

        #endregion

        #region Delete Sender\Recepient

        private void DeleteSender_Click(object sender, RoutedEventArgs e)
        {
            if (SendersBase.SelectedItems.Count == 0) return;
            SendersListBase.Remove((Sender)SendersBase.SelectedItem);
        }

        private void DeleteRecepient_Click(object sender, RoutedEventArgs e)
        {
            if (RecepientsBase.SelectedItems.Count == 0) return;
            RecepientsList.Remove((Recepient)RecepientsBase.SelectedItem);
        }

        #endregion


    }
}
