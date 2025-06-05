using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Model.Data;

namespace Model.Core
{
    public class SerializerXML : Serializer
    {
        public override void Save(Sizes world)
        {
            var serializer = new XmlSerializer(typeof(SizeDTO));
            using (var writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, new SizeDTO(world));
            }
        }

        public override void Load(Sizes world)
        {
            //var serializer = new XmlSerializer(typeof(SizeDTO));
            //using (var reader = new StreamReader(FilePath))
            //{
            //    var deser = (SizeDTO)serializer.Deserialize(reader);
            //    var massive = deser.Kletochkadto;
            //    var w = deser.Width;
            //    var h = deser.Height;
            //    var level = deser.Level;
            //    world.LoadField(w, h, level, massive);
            //}

        }
    }
}