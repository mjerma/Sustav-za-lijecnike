using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SustavZaLijecnike.ValidationRules
{
    class MaticniBrojRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), @"^[0-9]+$"))
            {
                return new ValidationResult(false, "Unesite podatke brojevima");
            }

            if (value.ToString().Length != 10)
            {
                return new ValidationResult(false, "Matični broj mora imati 10 znamenki!");
            }
            return new ValidationResult(true, null);
        }
    }
}
