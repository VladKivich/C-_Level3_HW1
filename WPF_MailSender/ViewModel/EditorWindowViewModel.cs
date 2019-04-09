using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MailSender.Interfaces;

namespace WPF_MailSender.ViewModel
{
    public class EditorWindowViewModel: ViewModelBase
    {
        #region Заголовок окна редактора

        public string _Title = "";

        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }

        #endregion

        public Sender Sender { get; set; }

        public Recepient Recepient { get; set; }

        private EditorWindow EW;

        public ICommand SaveRecepinetChange { get; }

        private readonly IRecepients RecepientsData;

        public EditorWindowViewModel(IRecepients RecepientsData)
        {
            this.RecepientsData = RecepientsData;

            SaveRecepinetChange = new RelayCommand<EditorWindow>(ActionButton);
        }

        private void ActionButton(EditorWindow EW)
        {
            this.EW = EW;

            if(EW.Mode == EditorWindow.EditorWindowShowMode.EditMode && EW.CurrentObject is Recepient)
            {
                Recepient R = EW.CurrentObject as Recepient;
                R.Email = EW.TextEmail.Text;
                SomeAction(R);
            }
        }

        private void SomeAction(Recepient Recepient)
        {
            RecepientsData.Edit(Recepient);
        }
    }
}
