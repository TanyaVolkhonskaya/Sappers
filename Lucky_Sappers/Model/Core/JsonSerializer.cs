using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Model.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class JsonSerializer : Serializer
    {
        protected override string Extension => "json";

        public override void Serialize<T>(T obj, string fileName)
        {
            SelectFile(fileName);
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(FilePath, json);
        }

        public override T Deserialize<T>(string fileName)
        {
            SelectFile(fileName);
            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

