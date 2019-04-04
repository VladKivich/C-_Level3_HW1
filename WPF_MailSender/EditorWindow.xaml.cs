using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
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
    /// Логика взаимодействия для EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        internal enum WindowShowMode
        {
            AddMode = 1,
            EditMode = 2
        }

        private static IList Collection;
        private Type ListObject;
        private Object EditObject;
        private WindowShowMode Mode;

        private EditorWindow(Window Owner, string Title, IList List)
        {
            InitializeComponent();

            this.Title = Title;
            this.Owner = Owner;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Collection = List;
        }

        private EditorWindow(Window Owner, IList List, Type ListObject, string Title) : this(Owner, Title, List)
        {
            WindowMode(ListObject);

            this.ListObject = ListObject;
            TextName.Focus();
            ActionButton.Content = "Add";
            Mode = WindowShowMode.AddMode;
            Show();
        }

        public EditorWindow(Window Owner, IList List, IEmail EditObject, string Title) : this(Owner, Title, List)
        {
            this.EditObject = EditObject;

            WindowMode(EditObject.GetType());

            TextName.Focus();

            ActionButton.Content = "Edit";
            Mode = WindowShowMode.EditMode;

            TextEmail.Text = EditObject.Email;

            if(EditObject is ISender)
            {
                TextName.Text = (EditObject as ISender).Name;
                TextPort.Text = (EditObject as ISender).Port.ToString();
                TextSMTP.Text = (EditObject as ISender).Server;
            }

            Show();
        }

        private void WindowMode(Type T)
        {
            if (T == typeof(Recepient))
            {
                TextName.IsEnabled = false;
                TextPort.IsEnabled = false;
                TextSMTP.IsEnabled = false;
                TextPassword.IsEnabled = false;
                TextEmail.Focus();
            }
        }

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

        internal static EditorWindow GetEditorWindow(Window owner, IList List, Type ListObject, string Title)
        {
            return new EditorWindow(owner, List, ListObject, Title);
        }

        internal static EditorWindow GetEditorWindow(Window owner, IList List, IEmail EditObject, string Title)
        {
            return new EditorWindow(owner, List, EditObject, Title);
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            switch(Mode)
            {
                case (WindowShowMode)1:
                    AddSome();
                    break;

                case (WindowShowMode)2:
                    EditSome();
                    break;
            }
        }

        private void AddSome()
        {
            if (ListObject == typeof(Sender))
            {
                if (IsNullOrEmpty(TextName.Text, TextPort.Text, TextSMTP.Text, TextEmail.Text))
                {
                    StaticVariables.GetNewMessageWindow(this, "Input Error", "Do not leave empty fields", Brushes.OrangeRed, Visibility.Hidden).ShowDialog();
                }
                else
                {
                    Collection.Add(new Sender(TextName.Text, TextEmail.Text.ToLower(), TextSMTP.Text.ToLower(), Convert.ToInt32(TextPort.Text), TextPassword.SecurePassword));
                    this.Close();
                }
            }

            else if (ListObject == typeof(Recepient))
            {
                if (IsNullOrEmpty(TextEmail.Text))
                {
                    StaticVariables.GetNewMessageWindow(this, "Input Error", "Do not leave empty fields", Brushes.OrangeRed, Visibility.Hidden).ShowDialog();
                }
                else
                {
                    Collection.Add(new Recepient(TextEmail.Text.ToLower()));
                    this.Close();
                }
            }
        }

        private void EditSome()
        {
            if (EditObject is ISender)
            {
                if (IsNullOrEmpty(TextName.Text, TextPort.Text, TextSMTP.Text, TextEmail.Text))
                {
                    StaticVariables.GetNewMessageWindow(this, "Input Error", "Do not leave empty fields", Brushes.OrangeRed, Visibility.Hidden).ShowDialog();
                }
                else
                {
                    Collection[Collection.IndexOf(EditObject)] = (EditObject as ISender).SetParameters(TextName.Text, TextEmail.Text, TextSMTP.Text, Convert.ToInt32(TextPort.Text), TextPassword.SecurePassword);
                    this.Close();
                }
            }

            else
            {
                if (IsNullOrEmpty(TextEmail.Text))
                {
                    StaticVariables.GetNewMessageWindow(this, "Input Error", "Do not leave empty fields", Brushes.OrangeRed, Visibility.Hidden).ShowDialog();
                }
                else
                {
                    Collection[Collection.IndexOf(EditObject)] = (EditObject as IEmail).SetParameters(TextEmail.Text);
                    this.Close();
                }
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
