using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using WPF_MailSender.Interfaces;
using WPF_MailSender.Services;

namespace WPF_MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Recepient> RecepientsList { get; } = new ObservableCollection<Recepient>();

        public ObservableCollection<Sender> SendersList { get; } = new ObservableCollection<Sender>();

        private WindowManager WM;

        private readonly ICorrespondents CorrespondentsData;

        #region Команды

        public ICommand LoadCorrespondentsDataCommand { get; }

        public ICommand LoadSendersDataCommand { get; }

        public ICommand NewRecepientCommand { get; }

        public ICommand EditRecepientCommand { get; }

        #endregion
        
        public MainViewModel(ICorrespondents CorrespondentsData, WindowManager windowManager)
        {
            //Менеджер окон
            WM = windowManager;
            
            //Заполняем коллекцию получателей из БД
            this.CorrespondentsData = CorrespondentsData;

            LoadCorrespondentsDataCommand = new RelayCommand(LoadCorrespondentsData);

            LoadCorrespondentsData();

            //Получаем данные об отправителях
            LoadSendersDataCommand = new RelayCommand(LoadSenders);

            LoadSenders();

            #region Команды создание\редактирования получателей\отправителей.

            NewRecepientCommand = new RelayCommand(NewRecepient);

            EditRecepientCommand = new RelayCommand(EditRecepient);

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
                CorrespondentsData.CurrentRecepient = SelectedRecepient;
                Set(ref _SelectedRecepient, value);
            }
        }

        #endregion
        
        /// <summary>
        /// Загружаем данные из БД в коллекцию получателей
        /// </summary>
        private void LoadCorrespondentsData()
        {
            RecepientsList.Clear();

            foreach (var item in CorrespondentsData.GetAllRecepients())
            {
                RecepientsList.Add(item);
            }
        }

        /// <summary>
        /// "Загружем" данные об отправителях.
        /// </summary>
        private void LoadSenders()
        {
            foreach (var item in CorrespondentsData.Senders)
            {
                SendersList.Add(item);
            }
        }

        /// <summary>
        /// Создает нового получателя
        /// </summary>
        private void NewRecepient()
        {
            Recepient R = new Recepient();

            if (WM.NewRecepient(R))
            {
                CorrespondentsData.AddNew(R.Email);
                LoadCorrespondentsData();
            }
        }

        /// <summary>
        /// Редактирует выбранного получателя
        /// </summary>
        private void EditRecepient()
        {
            if (SelectedRecepient is null) return;
            else
            {
                if(WM.EditRecepient(SelectedRecepient))
                {
                    CorrespondentsData.Edit(SelectedRecepient);
                    LoadCorrespondentsData();
                }
            }
        }
    }
}