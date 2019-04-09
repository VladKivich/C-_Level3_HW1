using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using WPF_MailSender.Interfaces;

namespace WPF_MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Recepient> RecepientsList { get; } = new ObservableCollection<Recepient>();

        private readonly IRecepients RecepientsData;

        public MainViewModel(IRecepients RecepientsData)
        {
            this.RecepientsData = RecepientsData;
            foreach (var item in RecepientsData.GetAllRecepients())
            {
                RecepientsList.Add(item);
            }
        }

        public string _Title = "WPF Mail Sender";

        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }

        
    }
}