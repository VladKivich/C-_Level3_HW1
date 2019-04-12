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

        #region �������

        public ICommand LoadCorrespondentsDataCommand { get; }

        public ICommand LoadSendersDataCommand { get; }

        public ICommand NewRecepientCommand { get; }

        public ICommand EditRecepientCommand { get; }

        public ICommand DeleteRecepientCommand { get; }

        #endregion

        public MainViewModel(ICorrespondents CorrespondentsData, WindowManager windowManager)
        {
            //�������� ����
            WM = windowManager;
            
            //��������� ��������� ����������� �� ��
            this.CorrespondentsData = CorrespondentsData;

            LoadCorrespondentsDataCommand = new RelayCommand(LoadCorrespondentsData);

            LoadCorrespondentsData();

            //�������� ������ �� ������������
            LoadSendersDataCommand = new RelayCommand(LoadSenders);

            LoadSenders();

            #region ������� ��������\��������������\�������� �����������\������������.

            NewRecepientCommand = new RelayCommand(NewRecepient);

            EditRecepientCommand = new RelayCommand(EditRecepient);

            DeleteRecepientCommand = new RelayCommand(DeleteRecepient);

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
        private void LoadCorrespondentsData()
        {
            RecepientsList.Clear();

            foreach (var item in CorrespondentsData.GetAllRecepients())
            {
                RecepientsList.Add(item);
            }
        }

        /// <summary>
        /// "��������" ������ �� ������������.
        /// </summary>
        private void LoadSenders()
        {
            foreach (var item in CorrespondentsData.Senders)
            {
                SendersList.Add(item);
            }
        }

        /// <summary>
        /// ������� ������ ����������
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
        /// ����������� ���������� ����������
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

        /// <summary>
        /// �������� ���������� ����������.
        /// </summary>
        private void DeleteRecepient()
        {
            if (SelectedRecepient is null) return;
            CorrespondentsData.Delete(SelectedRecepient);
            LoadCorrespondentsData();
        }
    }
}