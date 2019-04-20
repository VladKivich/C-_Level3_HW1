using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WPF_MailSender.Interfaces;
using WPF_MailSender.Services;
using WPF_MailSender.Data;

namespace WPF_MailSender.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewmodelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ICorrespondents, CorrespondentsData>();
            SimpleIoc.Default.Register(() => new MailSenderDBDataContext());
            SimpleIoc.Default.Register<EditorWindowViewModel>();
            SimpleIoc.Default.Register<WindowManager>();
            SimpleIoc.Default.Register<EmailsDataService>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public EditorWindowViewModel Editor => ServiceLocator.Current.GetInstance<EditorWindowViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the Viewmodels
        }
    }
}