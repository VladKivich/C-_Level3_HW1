using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MailSender.ViewModel;

namespace WPF_MailSender.Services
{
    public class WindowManager
    {
        EditorWindow EditorWindow;
        EditorWindowViewModel View;

        public bool EditRecepient(Recepient recepient)
        {
            CreateModelAndWindow("Recipient Editor", "Edit", EditorWindowMode.Recepient);

            EditorWindow.TextEmail.Focus();

            View.GotRecepient(recepient);

            if (EditorWindow.ShowDialog() != true) return false;

            recepient.Email = EditorWindow.TextEmail.Text;

            return true;
        }

        public bool NewRecepient(Recepient recepient)
        {
            CreateModelAndWindow("Recipient Creator", "Create", EditorWindowMode.Recepient);

            EditorWindow.TextEmail.Focus();

            View.GotRecepient(recepient);

            if (EditorWindow.ShowDialog() != true) return false;

            recepient.Email = EditorWindow.TextEmail.Text;

            return true;
        }

        public bool EditSender(Sender sender)
        {
            CreateModelAndWindow("Sender Editor", "Edit", EditorWindowMode.Sender);

            EditorWindow.TextName.Focus();

            View.GotSender(sender);

            if (EditorWindow.ShowDialog() != true) return false;

            sender.Name = EditorWindow.TextName.Text;
            sender.Email = EditorWindow.TextEmail.Text;
            sender.Server = EditorWindow.TextSMTP.Text;
            sender.Port = Convert.ToInt32(EditorWindow.TextPort.Text);
            sender.ID.UserName = sender.Email;
            sender.ID.Password = EditorWindow.TextPassword.Password;

            return true;
        }

        public bool NewSender(Sender sender)
        {
            CreateModelAndWindow("Sender Creator", "Create", EditorWindowMode.Sender);

            EditorWindow.TextName.Focus();

            View.GotSender(sender);

            if (EditorWindow.ShowDialog() != true) return false;

            sender.Name = EditorWindow.TextName.Text;
            sender.Email = EditorWindow.TextEmail.Text;
            sender.Server = EditorWindow.TextSMTP.Text;
            sender.Port = Convert.ToInt32(EditorWindow.TextPort.Text);
            sender.ID.UserName = sender.Email;
            sender.ID.Password = EditorWindow.TextPassword.Password;

            return true;
        }

        private void OnCLose(object sender, bool Result)
        {
            View.Closed -= OnCLose;
            EditorWindow.DialogResult = Result;
            EditorWindow.Close();
        }

        private void CreateModelAndWindow(string Title, string Button, EditorWindowMode Mode)
        {
            View = new EditorWindowViewModel(Title, Button, Mode);
            
            EditorWindow = new EditorWindow { DataContext = View };

            View.Closed += OnCLose;
        }
    }
}
