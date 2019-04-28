using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_MailSender
{
    /// <summary>
    /// Логика взаимодействия для UserMessageWindow.xaml
    /// </summary>
    public partial class UserMessageWindow : Window
    {
        private UserMessageWindow(Window Owner, string EmailTitle, string EmailText, SolidColorBrush Brush, Visibility ExitButton = Visibility.Visible)
        {
            InitializeComponent();
            this.Owner = Owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Title = EmailTitle;
            UserMessage.Text = EmailText;
            UserMessage.Foreground = Brush;
            CloseButton.Visibility = ExitButton;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Owner.Focus();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }

        public static UserMessageWindow GetMessageWindow(Window Owner, string EmailTitle, string EmailText, SolidColorBrush Brush, Visibility ExitButton = Visibility.Visible)
        {
            return new UserMessageWindow(Owner, EmailTitle, EmailText, Brush, ExitButton);
        }
    }
}
