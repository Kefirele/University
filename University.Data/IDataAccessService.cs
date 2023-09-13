using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Data
{
    public interface IDataAccessService<T>
    {
        void SaveData(IEnumerable<T> data);
        IEnumerable<T> LoadData();
    }
    public class JsonDataAccessService<T> : IDataAccessService<T>
{
    private readonly string _jsonFilePath;

    public JsonDataAccessService(string jsonFilePath)
    {
        _jsonFilePath = jsonFilePath;
    }

    public void SaveData(IEnumerable<T> data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(_jsonFilePath, jsonData);
    }

    public IEnumerable<T> LoadData()
    {
        if (File.Exists(_jsonFilePath))
        {
            string jsonData = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
        }
        else
        {
            return Enumerable.Empty<T>();
        }
    }
}
}
