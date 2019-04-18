using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_MailSender.Components
{
    public partial class ButtonsControl : UserControl
    {
        public ButtonsControl()
        {
            InitializeComponent();
        }

        #region CreateCommand : ICommand - Команда создания нового элемента

        /// <summary>Команда создания нового элемента</summary>
        public static readonly DependencyProperty CreateCommandProperty =
            DependencyProperty.Register(
                nameof(CreateCommand),
                typeof(ICommand),
                typeof(ButtonsControl),
                new PropertyMetadata(default(ICommand)));

        /// <summary>Команда создания нового элемента</summary>
        public ICommand CreateCommand
        {
            get => (ICommand)GetValue(CreateCommandProperty);
            set => SetValue(CreateCommandProperty, value);
        }

        #endregion

        #region EditCommand : ICommand - Команда редактирования выбранного элемента

        /// <summary>Команда редактирования выбранного элемента</summary>
        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register(
                nameof(EditCommand),
                typeof(ICommand),
                typeof(ButtonsControl),
                new PropertyMetadata(default(ICommand)));

        /// <summary>Команда редактирования выбранного элемента</summary>
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        #endregion

        #region DeleteCommand : ICommand - Команда удаления выбранного элемента

        /// <summary>Команда удаления выбранного элемента</summary>
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteCommand),
                typeof(ICommand),
                typeof(ButtonsControl),
                new PropertyMetadata(default(ICommand)));

        /// <summary>Команда удаления выбранного элемента</summary>
        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        #endregion
    }
}
