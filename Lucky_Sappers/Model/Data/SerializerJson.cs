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
        public override void Save(Sizes world)
        {
            var json = JObject.FromObject(new SizeDTO(world));

            File.WriteAllText(FilePath, json.ToString());
        }
        public override void Load(Sizes world) 
        {
            var content = File.ReadAllText(FilePath);
            var dto = JsonConvert.DeserializeObject<SizeDTO>(content);
            dto.Deserialize();
        }
    }
}

