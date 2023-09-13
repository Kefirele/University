using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using University.Data;

namespace University.Services.Tests
{
    [TestClass]
    public class JsonDataAccessServiceTests
    {
        private string jsonFilePath;
        private JsonDataAccessService<string> dataAccessService;

        [TestInitialize]
        public void Initialize()
        {
            jsonFilePath = "test.json";

            dataAccessService = new JsonDataAccessService<string>(jsonFilePath);
        }

        [TestMethod]
        public void SaveData_LoadData_Success()
        {
            var testData = new List<string> { "A", "B", "C" };

            dataAccessService.SaveData(testData);

            var loadedData = dataAccessService.LoadData();

            Assert.AreEqual(testData.Count, loadedData.Count());
        }

        [TestMethod]
        public void LoadData_FileNotExists_ReturnsEmptyCollection()
        {
            File.Delete(jsonFilePath);

            IEnumerable<string> loadedData = dataAccessService.LoadData();

            CollectionAssert.AreEqual(new List<string>(), loadedData.ToList());
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }
        }
    }
}