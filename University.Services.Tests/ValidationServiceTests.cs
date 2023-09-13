using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Tests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Service;

namespace University.Services.Tests
{
    [TestClass]
    public class ValidationServiceTests
    {
        private IValidationService validationService;

        [TestInitialize]
        public void Initialize()
        {
            validationService = new ValidationService();
        }
        [TestMethod]
        public void IsValidPESELAndBirthDate_ValidPESELAndBirthDate_ReturnsTrue()
        {
            string validPESEL = "94071312345";
            DateTime birthDate = new DateTime(1994, 7, 13);

            Debug.WriteLine($"validPESEL: {validPESEL}");
            Debug.WriteLine($"birthDate: {birthDate}");

            bool isValid = validationService.IsValidPESELAndBirthDate(validPESEL, birthDate);

            Debug.WriteLine($"isValid: {isValid}");

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidPESELAndBirthDate_InvalidPESEL_ReturnsFalse()
        {
            string invalidPESEL = "12345678901";
            DateTime birthDate = new DateTime(1990, 1, 1);

            bool isValid = validationService.IsValidPESELAndBirthDate(invalidPESEL, birthDate);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidPESELAndBirthDate_InvalidBirthDate_ReturnsFalse()
        {
            string validPESEL = "94071312345";
            DateTime invalidBirthDate = new DateTime(2000, 1, 1);

            bool isValid = validationService.IsValidPESELAndBirthDate(validPESEL, invalidBirthDate);

            Assert.IsFalse(isValid);
        }
    }
}
