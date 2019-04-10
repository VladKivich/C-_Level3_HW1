using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_MailSender.Data;

namespace WPF_MailSender
{

    /// <summary>
    /// Логика взаимодействия для EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public enum EditorWindowShowMode
        {
            CreateMode = 1,
            EditMode = 2
        }
        
        public EditorWindowShowMode Mode { get; private set; }

        public Object CurrentObject { get; private set; }

        private EditorWindow(Window Owner, string Title, EditorWindowShowMode WidnowMode)
        {
            InitializeComponent();

            this.Title = Title;
            this.Owner = Owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.Mode = WidnowMode;
        }

        private EditorWindow(Window Owner, string Title, EditorWindowShowMode WidnowMode, Sender Sender) : this(Owner, Title, WidnowMode)
        {
            WindowMode(Sender);
            CurrentObject = Sender;
            Show();
        }

        private EditorWindow(Window Owner, string Title, EditorWindowShowMode WidnowMode, Recepient Recepient) : this(Owner, Title, WidnowMode)
        {
            WindowMode(Recepient);
            CurrentObject = Recepient;
            Show();
        }

        public static EditorWindow GetNewWindow(Window Owner, string Title, EditorWindowShowMode WidnowMode, Sender Sender) => new EditorWindow(Owner, Title, WidnowMode, Sender);

        public static EditorWindow GetNewWindow(Window Owner, string Title, EditorWindowShowMode WidnowMode, Recepient Recepient) => new EditorWindow(Owner, Title, WidnowMode, Recepient);

        #region Режимы окна редактора

        private void WindowMode(Sender Sender)
        {
            TextName.Focus();

            switch (Mode)
            {
                case (EditorWindowShowMode)2:
                    TextName.Text = Sender.Name;
                    TextEmail.Text = Sender.ID.UserName;
                    TextSMTP.Text = Sender.Server;
                    TextPort.Text = Sender.Port.ToString();
                    TextPassword.Password = Sender.ID.Password;
                    ActionButton.Content = "Edit";
                    break;

                default:
                    ActionButton.Content = "Create";
                    break;
            }
        }

        private void WindowMode(Recepient Recepient)
        {
            TextName.IsEnabled = false;
            TextSMTP.IsEnabled = false;
            TextPort.IsEnabled = false;
            TextPassword.IsEnabled = false;

            TextEmail.Focus();

            switch (Mode)
            {
                case (EditorWindowShowMode)2:
                    TextEmail.Text = Recepient.Email;
                    ActionButton.Content = "Edit";
                    break;

                default:
                    ActionButton.Content = "Create";
                    break;
            }
        }

        #endregion
        
        private void TextPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            int result;
            if (String.IsNullOrEmpty(TextPort.Text)) return;
            if (!Int32.TryParse(TextPort.Text, out result))
            {
                TextPort.Text = null;
                StaticVariables.GetNewMessageWindow(this, "Input Error", "Please input correct port! Digits Only", Brushes.OrangeRed, Visibility.Hidden).ShowDialog();
            }
        }

        private bool IsNullOrEmpty(params string[] values)
        {
            foreach(string s in values)
            {
                if (String.IsNullOrEmpty(s)) return true;
            }
            return false;
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
