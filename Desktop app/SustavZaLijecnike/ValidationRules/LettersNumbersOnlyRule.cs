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
    class LettersNumbersOnlyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), @"^[a-zA-ZšŠđĐžŽčČćĆ0-9\s]+$"))
            {
                return new ValidationResult(false, "Unesite podatke bez posebnih znakova");
            }

            return new ValidationResult(true, null);
        }
    }
}
