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
            CreateModelAndWindow("Recipient Editor", "Edit", EditorWindowMode.Recepient, recepient);
            
            if (EditorWindow.ShowDialog() != true) return false;

            recepient.Email = EditorWindow.TextEmail.Text;

            return true;
        }

        public bool NewRecepient(Recepient recepient)
        {
            CreateModelAndWindow("Recipient Creator", "Create", EditorWindowMode.Recepient, recepient);
            
            if (EditorWindow.ShowDialog() != true) return false;

            recepient.Email = EditorWindow.TextEmail.Text;

            return true;
        }

        public void OnCLose(object sender, bool Result)
        {
            View.Closed -= OnCLose;
            EditorWindow.DialogResult = Result;
            EditorWindow.Close();
        }

        private void CreateModelAndWindow(string Title, string Button, EditorWindowMode Mode, Recepient recepient)
        {
            View = new EditorWindowViewModel(Title, Button, EditorWindowMode.Recepient);

            View.SendRecepient(recepient);

            EditorWindow = new EditorWindow { DataContext = View };

            View.Closed += OnCLose;
        }
    }
}
