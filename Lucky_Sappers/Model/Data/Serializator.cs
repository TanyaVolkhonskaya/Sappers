using Lucky_Sappers;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Model
{
    public class DataGame
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Datakl[,] kl {  get; set; }
    }
    public class Datakl
    {
        public bool IsBomb { get;  set; }
        public bool IsFlagged { get; set; }
        public bool IsDigit { get; set; }
        public int Counter { get; set; }
    }
    }
    public class JsonSer
    {
        public void Save (DataGame dataGame,string filePath)
    {
        var jo = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };
        var json = JsonConvert.SerializeObject (dataGame, jo);
    
    }
    public DataGame Load(string filePath)
    {
        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<DataGame>(json);
    }

}
