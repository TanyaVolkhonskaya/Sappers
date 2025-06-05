
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model.Data;
using Model.Core;
using Newtonsoft.Json.Serialization;

namespace Model.Data
{
    delegate string MathOperation(string fileName);
    public interface ISerializerTop
    {
        string FolderPath { get; }
        string FileP { get; }
        string SelectFile(string name);
    }
    public abstract class SerializerTop : ISerializerTop
    {
        public string FolderPath { get; private set; }
        public string FileP { get; private set; }
        public abstract string Extension { get; }

        public string SelectFile(string name)
        {
            var name_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name}.{Extension}");
            if (File.Exists(name_file) == false)
            {
                File.Create(name_file).Close();
            }
            FileP = name_file;
            return FileP;
        }
    }

}