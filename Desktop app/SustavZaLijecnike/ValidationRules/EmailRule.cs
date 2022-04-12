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
    class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!Regex.IsMatch(value.ToString(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                return new ValidationResult(false, "Unesite ispravan email");
            }

            return new ValidationResult(true, null);
        }
    }
}
