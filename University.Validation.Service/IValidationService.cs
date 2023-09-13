using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Validation.Service
{
    public interface IValidationService
    {
        bool IsValidPESEL(string pesel);
        bool IsValidBirthDate(string pesel, DateTime? birthDate);
    }
}
