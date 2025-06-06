using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Model.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Model.Data
{
    public class JsonSerializer : Serializer
    {

        public override string Extention => ".json";

        public override void Save(Sizes siziki)
        {
            
            SetFilePath(FilePath);
            var set = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            var json = JsonConvert.SerializeObject(siziki,set);
            File.WriteAllText(FilePath, json);
        }
        public override Sizes Load(string siziki) 
        {

            string json = File.ReadAllText(siziki);
            return JsonConvert.DeserializeObject<Sizes>(json);
            var content = File.ReadAllText(siziki);
            var deser =JObject.Parse(content);
            var Width = deser["Width"].ToObject<int>();
            var Height = deser["Height"].ToObject<int>();
            var kletochka = deser["Kletochka"].ToObject<Kletka[,]>();
            var level = deser["Level"].ToObject<int>();
            var lose = deser["Lose"].ToObject<bool>();
            var win = deser["Win"].ToObject<bool>();
            var Timer = deser["Timer"].ToObject<int>();
            
            return new Sizes(Width, Height, level, Timer,kletochka);
        }
    }
}

