using System;
using System.Collections;
using System.Collections.ObjectModel;
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
        public EditorWindow(Window Owner)
        {
            this.Owner = Owner;
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

        public bool HasErrors()
        {
            foreach (DependencyObject item in StackPanel.Children)
            {
                if (Validation.GetHasError(item)) return true;
            }
            return false;
        }
    }
}
