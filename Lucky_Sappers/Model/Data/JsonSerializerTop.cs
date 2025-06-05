using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Model.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static Model.Data.XML_TOP_S;

namespace Model.Data
{
    public class JSON_SerializerList : SerializerTop
    {
        public override string Extension
        {
            get
            {
                return "json";
            }
        }
        public int[] Deserialize()
        {
            Name_Files operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            var text = File.ReadAllText(FilePath);
            var top = JsonConvert.DeserializeObject<Top>(text);
            if (top == null) return new int[0];
            int[] Topper = top.Top_10;
            return Topper;
        }

        public void Serialize(int result)
        {
            Name_Files operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            int[] text = Deserialize();
            Array.Resize(ref text, text.Length + 1);
            text[text.Length - 1] = result;
            int[] newtop = text.OrderBy(x=>x).Take(10).ToArray();
            var xml = new XML_TOP_S();
            xml.S_TOP_10(newtop);
            Top top = new Top
            {
                Top_10 = newtop

            };
            string js = JsonConvert.SerializeObject(top);

            File.WriteAllText(FilePath, js);
        }
        public void Serialize(int[] top_10)
        {
            Name_Files operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            Top top = new Top
            {
                Top_10 = top_10

            };
            string json_convert = JsonConvert.SerializeObject(top);

            File.WriteAllText(FilePath, json_convert);
        }
        private class Top
        {
            public int[] Top_10 { get; set; }
            public int Top_1 { get; set; }
        }
    }
}