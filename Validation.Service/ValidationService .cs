using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Service;

namespace Services.Tests
{
    public class ValidationService : IValidationService
    {
        public bool IsValidPESELAndBirthDate(string pesel, DateTime? birthDate)
        {
            if (pesel.Length != 11 || !birthDate.HasValue)
            {
                return false;
            }

            string yearPart = pesel.Substring(0, 2);
            string monthPart = pesel.Substring(2, 2);
            string dayPart = pesel.Substring(4, 2);

            int year;
            int month;
            int day;

            if (!int.TryParse(yearPart, out year) || !int.TryParse(monthPart, out month) || !int.TryParse(dayPart, out day))
            {
                return false;
            }

            if (month > 80)
            {
                year += 1800;
                month -= 80;
            }
            else if (month > 20)
            {
                year += 2000;
                month -= 20;
            }
            else
            {
                year += 1900;
            }

            try
            {
                DateTime parsedDate = new DateTime(year, month, day);
                return parsedDate == birthDate.Value;
            }
            catch
            {
                return false;
            }
        }


    }
}
