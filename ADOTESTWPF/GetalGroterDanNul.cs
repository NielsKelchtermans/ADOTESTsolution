using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace ADOTESTWPF
{
    public class GetalGroterDanNul : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal getal;
            NumberStyles style = NumberStyles.Currency;
            if (value == null || ((string)value).Length == 0)
            {
                return new ValidationResult(false, "Getal moet ingevuld zijn");
            }
            if (!decimal.TryParse(value.ToString(), style, cultureInfo, out getal))
            {
                return new ValidationResult(false, "Waarde moet een getal zijn");
            }
            if (getal <= 0)
            {
                return new ValidationResult(false, "Waarde moet positief zijn");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}