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
    public class XML_TOP_S : SerializerTop
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
            Name_Files operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Top");
            XmlSerializer top_10 = new XmlSerializer(typeof(Top));
            Top top;
            using (StreamReader writer = new StreamReader(FilePath))
            {
                top = (Top)top_10.Deserialize(writer);
            }
            int[] Topers = top.Top_10;
            return Topers;
        }
        public void Serializer_TOP(int result)
        {
            Name_Files operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Top");
            int[] text = Deserialize();
            Array.Resize(ref text, text.Length + 1);
            text[text.Length - 1] = result;
            int[] newtop = text;

            for (int j = 0; j < newtop.Length; j++)
            {
                for (int i = 0; i < newtop.Length - 1; i++)
                {
                    if (newtop[i] < newtop[i + 1])
                    {
                        int x = newtop[i];
                        newtop[i] = newtop[i + 1];
                        newtop[i + 1] = x;
                    }
                }
            }
            newtop = newtop.Take(10).ToArray();
            var json = new JSON_SerializerList();
            json.Serialize(newtop);
            Top top = new Top(newtop);
            XmlSerializer xml_ser = new XmlSerializer(typeof(Top));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xml_ser.Serialize(wr, top);
            }
        }
       
        public void S_TOP_10(int[] top_10) 
        {
            Name_Files operation = SelectFile;
            string FilePath = operation($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Top");
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
            public Top(int[] top)
            {
                Top_10 = top;
            }
        }
    }
}