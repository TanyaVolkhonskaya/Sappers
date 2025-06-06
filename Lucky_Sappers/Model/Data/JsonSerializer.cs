using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Model.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static Model.Data.XML_SerializerList;

namespace Model.Data
{
    public class JSON_SerializerList : Serializer
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
            MathOperation operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            var text = File.ReadAllText(FilePath);
            var top = JsonConvert.DeserializeObject<Top>(text);

            if (top == null) return new int[0];

            int[] top_top = top.Top_10;
            return top_top;
        }

        public void Serialize(int top_1)
        {
            MathOperation operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            int[] text = Deserialize();
            Array.Resize(ref text, text.Length + 1);
            text[text.Length - 1] = top_1;
            int[] new_top = text;

            for (int j = 0; j < new_top.Length; j++)
            {
                for (int i = 0; i < new_top.Length - 1; i++)
                {
                    if (new_top[i] < new_top[i + 1])
                    {
                        int x = new_top[i];
                        new_top[i] = new_top[i + 1];
                        new_top[i + 1] = x;
                    }
                }
            }
            new_top = new_top.Take(10).ToArray();
            var xml = new XML_SerializerList();
            xml.Serializer_top_10(new_top);
            Top top = new Top
            {
                Top_10 = new_top

            };
            string js = JsonConvert.SerializeObject(top);

            File.WriteAllText(FilePath, js);
        }
        public void Serialize(int[] top_10)
        {
            MathOperation operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            Top top = new Top
            {
                Top_10 = top_10

            };
            string js = JsonConvert.SerializeObject(top);

            File.WriteAllText(FilePath, js);
        }
        private class Top
        {
            public int[] Top_10 { get; set; }
            public int Top_1 { get; set; }
        }
    }
}