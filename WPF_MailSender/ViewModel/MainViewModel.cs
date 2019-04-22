using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using WPF_MailSender.Interfaces;
using WPF_MailSender.Models;
using WPF_MailSender.Services;

namespace WPF_MailSender.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private enum TabOrder
        {
            SendersAndRecepients = 0,
            Message = 1,
            Scheduler = 2
        }

        public ObservableCollection<Recepient> RecepientsList { get; } = new ObservableCollection<Recepient>();

        public ObservableCollection<Sender> SendersList { get; } = new ObservableCollection<Sender>();

        public ObservableCollection<Recepient> UserRecepientsList { get; } = new ObservableCollection<Recepient>();

        public ObservableCollection<Sender> UserSendersList { get; } = new ObservableCollection<Sender>();

        public ObservableCollection<EmailMessage> EmailMessagesList { get; } = new ObservableCollection<EmailMessage>();

        private WindowManager _WindowManager;

        private readonly ICorrespondents CorrespondentsData;

        private readonly IMessages EmailsData;

        #region Команды

        #region Команды "загрузки" данных

        public ICommand LoadCorrespondentsDataCommand { get; }

        public ICommand LoadSendersDataCommand { get; }

        #endregion

        #region Команды управления кнопками вкладки Senders/Recepients

        public ICommand NewRecepientCommand { get; }

        public ICommand EditRecepientCommand { get; }

        public ICommand DeleteRecepientCommand { get; }

        public ICommand NewSenderCommand { get; }

        public ICommand EditSenderCommand { get; }

        public ICommand DeleteSenderCommand { get; }

        public ICommand AddToSendersList { get; }

        public ICommand RemoveFromSendersList { get; }

        public ICommand AddToRecepientsList { get; }

        public ICommand RemoveFromRecepientsList { get; }

        public ICommand CreateNewTask { get; }

        #endregion

        #region Команды управления кнопками вкладки Messages

        public ICommand NewScheduler { get; }

        public ICommand NewMessage { get; }

        public ICommand DeleteMessage { get; }

        #endregion
        
        #endregion

        public MainViewModel(ICorrespondents CorrespondentsData, WindowManager windowManager, EmailsDataService EmailsService)
        {
            #region Получаем и загружаем данные

            //Менеджер окон
            _WindowManager = windowManager;

            //Заполняем коллекцию получателей из БД
            this.CorrespondentsData = CorrespondentsData;

            //Получаем все сообщения.
            EmailsData = EmailsService;

            LoadEmailMessages();

            LoadCorrespondentsData();

            //Получаем данные об отправителях и получателях
            LoadCorrespondentsDataCommand = new RelayCommand(LoadCorrespondentsData);

            LoadSendersDataCommand = new RelayCommand(LoadSenders);

            LoadSenders();

            #endregion

            #region Команды создание\редактирования\удаление получателей\отправителей.

            NewRecepientCommand = new RelayCommand(NewRecepient);

            EditRecepientCommand = new RelayCommand(EditRecepient);

            DeleteRecepientCommand = new RelayCommand(DeleteRecepient);

            NewSenderCommand = new RelayCommand(NewSender);

            EditSenderCommand = new RelayCommand(EditSender);

            DeleteSenderCommand = new RelayCommand(DeleteSender);

            #endregion

            #region Команды Добавления\Удаления Отправителя\Получателей в список.

            AddToSendersList = new RelayCommand(AddSenderToUserList);

            RemoveFromSendersList = new RelayCommand(ClearUserSendersList);

            AddToRecepientsList = new RelayCommand(AddRecepientToUserList);

            RemoveFromRecepientsList = new RelayCommand(RemoveRecepientFromUserList);

            #endregion

            #region Команды отправки\Переключения между вкладками главного окна

            CreateNewTask = new RelayCommand(NewTask);

            NewScheduler = new RelayCommand(SchedulerTab);

            #endregion

            #region Команды вкладки редактора писем

            NewMessage = new RelayCommand(CreateNewMessage);

            DeleteMessage = new RelayCommand<EmailMessage>(OnDeleteMessage);

            #endregion
        }
        
        /// <summary>
        /// Удаляем выбранное сообщение
        /// </summary>
        /// <param name="message"></param>
        private void OnDeleteMessage(EmailMessage message)
        {
            if (message is null) return;
            {
                EmailsData.Remove(message.ID);
                LoadEmailMessages();
            }
        }

        /// <summary>
        /// Создаем новое сообщение
        /// </summary>
        private void CreateNewMessage()
        {
            EmailsData.Add(new EmailMessage());
            LoadEmailMessages();
            SelectedEmailMessage = EmailsData.GetById(EmailsData.GetAll().Count);
        }

        /// <summary>
        /// Переход на вкладку планировщика
        /// </summary>
        private void SchedulerTab()
        {
            if(SelectedEmailMessage != null)
            {
                //Переходим на вкладку планировщика
                SelectedTab = (int)TabOrder.Scheduler;
                SchedulerTabEnabled = true;
            }
        }

        /// <summary>
        /// Переход на следующую вкладку
        /// </summary>
        private void NewTask()
        {
            if (UserRecepientsList.Count == 0 | UserSendersList.Count == 0) return;
            else
            {
                //Переходим на вкладку редактора сообщений
                SelectedTab = (int)TabOrder.Message;
                MessageTabEnabled = true;
            }
        }

        /// <summary>
        /// Добавляем отправителя в список пользователя
        /// </summary>
        private void AddSenderToUserList()
        {
            if (SelectedSender != null & UserSendersList.Count == 0)
            {
                UserSendersList.Add(SelectedSender);
            }
        }

        /// <summary>
        /// Очистить список пользователей
        /// </summary>
        private void ClearUserSendersList()
        {
            UserSendersList.Clear();

            //Выключаем вкладки
            MessageTabEnabled = false;
        }

        /// <summary>
        /// Добавить получателя в список пользователя
        /// </summary>
        private void AddRecepientToUserList()
        {
            if (SelectedRecepient != null & !UserRecepientsList.Contains(SelectedRecepient))
            {
                UserRecepientsList.Add(SelectedRecepient);
            }
        }

        /// <summary>
        /// Удалить получателя из списка
        /// </summary>
        private void RemoveRecepientFromUserList()
        {
            if (_SelectedUserRecepient is null) return;
            else
            {
                UserRecepientsList.Remove(_SelectedUserRecepient);
                //Выключаем возможность редактирования вкладки с письмами при пустом списке получателей
                if (UserRecepientsList.Count == 0)
                {
                    MessageTabEnabled = false;
                }
            }
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

        private Recepient _SelectedUserRecepient;

        public Recepient SelectedUserRecepient
        {
            get { return _SelectedUserRecepient; }
            set
            {
                Set(ref _SelectedUserRecepient, value);
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

        #region Выбрайнное письмо

        private EmailMessage _SelectedEmailMessage;

        public EmailMessage SelectedEmailMessage
        {
            get { return _SelectedEmailMessage; }

            set
            {
                Set(ref _SelectedEmailMessage, value);
            }
        }

        #endregion

        #region Управление вкладками

        private int _SelectedTab = 0;

        public int SelectedTab
        {
            get
            {
                return _SelectedTab;
            }

            set
            {
                Set(ref _SelectedTab, value);
            }
        }
        
        #endregion

        #region Видимость вкладок

        private bool _MessageTabEnabled = false;

        public bool MessageTabEnabled
        {
            get => _MessageTabEnabled;
            set
            {
                Set(ref _MessageTabEnabled, value);
            }
        }
        

        private bool _SchedulerTabEnabled = false;

        public bool SchedulerTabEnabled { get => _SchedulerTabEnabled; set => Set(ref _SchedulerTabEnabled, value); }

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
        /// Получаем все сообщения
        /// </summary>
        private void LoadEmailMessages()
        {
            EmailMessagesList.Clear();

            foreach (var Email in EmailsData.GetAll())
            {
                EmailMessagesList.Add(Email);
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

            if (_WindowManager.NewSender(S))
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