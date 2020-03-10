using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace ADOTESTWPF
{
    public class GenreMoetGekozenWorden : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((value.ToString()).Length == 0 || value == null)
            {
                return new ValidationResult(false, "Genre moet gekozen worden");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}