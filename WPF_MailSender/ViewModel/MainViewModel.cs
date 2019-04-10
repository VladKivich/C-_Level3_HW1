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
            //�������� ������ � �����������
            this.RecepientsData = RecepientsData;

            LoadRecepientsDataCommand = new RelayCommand(LoadRecepientsData);

            LoadRecepientsData();

            //�������� ������ �� ������������
            LoadSendersDataCommand = new RelayCommand(LoadSenders);

            LoadSenders();

            #region ������� ��������\�������������� �����������\������������.

            NewRecepientCommand = new RelayCommand(NewRecepient);

            EditRecepientCommand = new RelayCommand<Recepient>(EditRecepient);

            #endregion
        }

        #region ��������� �������� ����

        public string _Title = "WPF Mail Sender";

        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }

        #endregion
        
        #region ��������� ����������

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
        /// ��������� ������ �� �� � ��������� �����������
        /// </summary>
        private void LoadRecepientsData()
        {
            foreach (var item in RecepientsData.GetAllRecepients())
            {
                RecepientsList.Add(item);
            }
        }

        /// <summary>
        /// "��������" ������ �� ������������.
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