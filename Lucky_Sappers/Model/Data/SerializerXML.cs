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
            using (var writer = new StreamWriter(FileP))
            {
                serializer.Serialize(writer, new SizeDTO(world));
            }
        }

        public override void Load(Sizes world)
        {
            using (var writer = new StreamReader(FileP))
            {
                var serializer = new XmlSerializer(typeof(SizeDTO));
                var dto = (SizeDTO)serializer.Deserialize(writer);
                
            }
        }

        }
    }
