using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SustavZaLijecnike.ValidationRules
{
    class ComboboxSelectionRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Equals(""))
            {
                return new ValidationResult(false, "Selection is invalid.");
            }

            return new ValidationResult(true, null);
        }
    }
}