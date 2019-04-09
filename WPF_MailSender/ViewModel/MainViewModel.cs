using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using WPF_MailSender.Interfaces;

namespace WPF_MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Recepient> RecepientsList { get; } = new ObservableCollection<Recepient>();

        public ObservableCollection<Sender> SendersList { get; } = new ObservableCollection<Sender>();

        private readonly IRecepients RecepientsData;

        public ICommand LoadRecepientsDataCommand { get; }

        public ICommand LoadSendersDataCommand { get; }

        public ICommand NewRecepientCommand { get; }

        public ICommand EditRecepientCommand { get; }

        public MainViewModel(IRecepients RecepientsData)
        {
            //Получаем данные о получателях
            this.RecepientsData = RecepientsData;

            LoadRecepientsDataCommand = new RelayCommand(LoadRecepientsData);

            LoadRecepientsData();

            //Получаем данные об отправителях
            LoadSendersDataCommand = new RelayCommand(LoadSenders);

            LoadSenders();

            #region Команды создание\редактирования получателей\отправителей.

            NewRecepientCommand = new RelayCommand(NewRecepient);

            EditRecepientCommand = new RelayCommand<Recepient>(EditRecepient);

            #endregion
        }

        #region Заголовок главного окна

        public string _Title = "WPF Mail Sender";

        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }

        #endregion
        
        #region Выбранный получатель

        private Recepient _SelectedRecepient;

        public Recepient SelectedRecepient
        {
            get { return _SelectedRecepient; }
            set
            {
                Set(ref _SelectedRecepient, value);
            }
        }

        #endregion
        
        /// <summary>
        /// Загружаем данные из БД в коллекцию получателей
        /// </summary>
        private void LoadRecepientsData()
        {
            foreach (var item in RecepientsData.GetAllRecepients())
            {
                RecepientsList.Add(item);
            }
        }

        /// <summary>
        /// "Загружем" данные об отправителях.
        /// </summary>
        private void LoadSenders()
        {
            foreach (var item in StaticVariables.SendersList)
            {
                SendersList.Add(item);
            }
        }

        private void NewRecepient()
        {
            StaticVariables.GetNewEditorWindow(StaticVariables.GetMainWindow, "Create New Recepient", EditorWindow.EditorWindowShowMode.CreateMode, new Recepient());
        }

        private void EditRecepient(Recepient SelectedRecepient)
        {
            if (SelectedRecepient == null) return;
            StaticVariables.GetNewEditorWindow(StaticVariables.GetMainWindow, "Create New Recepient", EditorWindow.EditorWindowShowMode.EditMode, SelectedRecepient);
        }
    }
}