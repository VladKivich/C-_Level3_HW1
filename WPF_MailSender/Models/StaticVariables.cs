using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static WPF_MailSender.EditorWindow;

namespace WPF_MailSender
{
    static class StaticVariables
    {
        public static UserMessageWindow GetNewMessageWindow(Window Owner, string EmailTitle, string EmailText, SolidColorBrush Brush, Visibility Exit = Visibility.Visible)
        {
            return UserMessageWindow.GetMessageWindow(Owner, EmailTitle, EmailText, Brush, Exit);
        }
    }
}
