using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ADOTESTWPF
{
    public class InVoorraadValidation2 : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal getal;

            if (value == null || ((string)value).Length == 0)
            {
                return new ValidationResult(false, "Getal moet ingevuld zijn");
            }
            if (!decimal.TryParse(value.ToString(), out getal))
            {
                return new ValidationResult(false, "Waarde moet een getal zijn");
            }
            if (getal < 0)
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
