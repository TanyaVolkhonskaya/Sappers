using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Model.Core;
using Model.Data;

namespace Model.Data
{
    public class XML_SerializerList : SerializerTop
    {
        public override string Extension
        {
            get
            {
                return "xml";
            }
        }
        public int[] Deserialize()
        {
            MathOperation operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            XmlSerializer top_10 = new XmlSerializer(typeof(Top));
            Top top;
            using (StreamReader writer = new StreamReader(FilePath))
            {
                top = (Top)top_10.Deserialize(writer);
            }
            int[] top_top = top.Top_10;
            return top_top;
        }
        public void Serializer_top_10(int top_1)
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
            var json = new JSON_SerializerList();
            json.Serialize(new_top);
            Top top = new Top(new_top);
            XmlSerializer xml_ser = new XmlSerializer(typeof(Top));
            using (StreamWriter writ = new StreamWriter(FilePath))
            {
                xml_ser.Serialize(writ, top);
            }
        }
        public void Serializer_top_10(int[] top_10)
        {
            MathOperation operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/top");
            Top top = new Top(top_10);
            XmlSerializer xml_ser = new XmlSerializer(typeof(Top));
            using (StreamWriter writ = new StreamWriter(FilePath))
            {
                xml_ser.Serialize(writ, top);
            }
        }
        public class Top
        {
            public int[] Top_10 { get; set; }
            public Top() { }
            public Top(int[] top_10)
            {
                Top_10 = top_10;
            }
        }
    }
}