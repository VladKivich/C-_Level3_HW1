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
        public EditorWindow()
        {
            InitializeComponent();
        }

        private void OnValidationError(object Sender, ValidationErrorEventArgs e)
        {
            if (!(e.Source is Control)) return;

            if (e.Action == ValidationErrorEventAction.Added)
            {
                Control control = e.Source as Control;
                control.ToolTip = e.Error.ErrorContent.ToString();
            }
        }
    }
}
