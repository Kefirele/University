using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.Service
{
    public interface IValidationService
    {
        bool IsValidPESELAndBirthDate(string pesel, DateTime? birthDate);
    }
}
