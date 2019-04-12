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

        private WindowManager _WindowManager;

        private readonly ICorrespondents CorrespondentsData;

        #region Команды

        public ICommand LoadCorrespondentsDataCommand { get; }

        public ICommand LoadSendersDataCommand { get; }

        public ICommand NewRecepientCommand { get; }

        public ICommand EditRecepientCommand { get; }

        public ICommand DeleteRecepientCommand { get; }

        public ICommand NewSenderCommand { get; }

        public ICommand EditSenderCommand { get; }

        public ICommand DeleteSenderCommand { get; }

        #endregion

        public MainViewModel(ICorrespondents CorrespondentsData, WindowManager windowManager)
        {
            //Менеджер окон
            _WindowManager = windowManager;
            
            //Заполняем коллекцию получателей из БД
            this.CorrespondentsData = CorrespondentsData;

            LoadCorrespondentsDataCommand = new RelayCommand(LoadCorrespondentsData);

            LoadCorrespondentsData();

            //Получаем данные об отправителях
            LoadSendersDataCommand = new RelayCommand(LoadSenders);

            LoadSenders();

            #region Команды создание\редактирования\удаление получателей\отправителей.

            NewRecepientCommand = new RelayCommand(NewRecepient);

            EditRecepientCommand = new RelayCommand(EditRecepient);

            DeleteRecepientCommand = new RelayCommand(DeleteRecepient);

            NewSenderCommand = new RelayCommand(NewSender);

            EditSenderCommand = new RelayCommand(EditSender);

            DeleteSenderCommand = new RelayCommand(DeleteSender);

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

        #region Выбранный отправитель

        private Sender _SelectedSender;

        public Sender SelectedSender
        {
            get { return _SelectedSender; }

            set
            {
                Set(ref _SelectedSender, value);
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
            SendersList.Clear();

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

            if (_WindowManager.NewRecepient(R))
            {
                CorrespondentsData.AddNewRecepient(R.Email);
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
                if(_WindowManager.EditRecepient(SelectedRecepient))
                {
                    CorrespondentsData.Edit(SelectedRecepient);
                    LoadCorrespondentsData();
                }
            }
        }

        /// <summary>
        /// Удаление выбранного получателя.
        /// </summary>
        private void DeleteRecepient()
        {
            if (SelectedRecepient is null) return;
            CorrespondentsData.Delete(SelectedRecepient);
            LoadCorrespondentsData();
        }

        /// <summary>
        /// Редактирует выбранного отправителя.
        /// </summary>
        private void EditSender()
        {
            if (SelectedSender is null) return;
            else
            {
                if (_WindowManager.EditSender(SelectedSender))
                {
                    CorrespondentsData.Edit(SelectedSender);
                    LoadSenders();
                }
            }
        }

        /// <summary>
        /// Создает нового отправителя.
        /// </summary>
        private void NewSender()
        {
            Sender S = new Sender();

            if (_WindowManager.EditSender(S))
            {
                CorrespondentsData.AddNewSender(S);
                LoadSenders();
            }
        }

        /// <summary>
        /// Удаляет выбранного отправителя.
        /// </summary>
        private void DeleteSender()
        {
            if (SelectedSender is null) return;
            CorrespondentsData.Delete(SelectedSender);
            LoadSenders();
        }
    }
}