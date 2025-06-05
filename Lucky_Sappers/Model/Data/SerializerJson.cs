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
        public override void Save(Sizes siziki)
        {
            SetFilePath(FilePath);
            var json = JObject.FromObject(new SizeDTO(siziki));

            File.WriteAllText(FilePath, json.ToString());
        }
        public override Sizes Load(string siziki) 
        {
            var content = File.ReadAllText(FilePath);
            var deser =JObject.Parse(content);
            var Width = deser["width"].ToObject<int>();
            var Height = deser["height"].ToObject<int>();
            var kletochka = deser["kletochka"].ToObject<Kletka[,]>();
            var level = deser["level"].ToObject<int>();
            var lose = deser["lose"].ToObject<bool>();
            var win = deser["win"].ToObject<bool>();
            var Timer = deser["timer"].ToObject<int>();
            
            return new Sizes(Width, Height, level, Timer);
        }
    }
}

