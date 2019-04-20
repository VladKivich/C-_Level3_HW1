using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_MailSender.Validation
{
    class ValidationsRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Name = value.ToString();

            if(Name.Length < 5 | Name.Length == 0)
            {
                return new ValidationResult(false, "Имя должно состоять минимум из 5 символов");

            }

            return new ValidationResult(true, null);
        }
    }
}
