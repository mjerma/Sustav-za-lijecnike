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
    class LettersOnlyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), @"^[a-zA-ZšŠđĐžŽčČćĆ\s]+$"))
            {
                return new ValidationResult(false, "Unesite podatke slovima");
            }

            return new ValidationResult(true, null);
        }
    }
}
