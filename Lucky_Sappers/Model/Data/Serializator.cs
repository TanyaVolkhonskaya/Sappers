
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
    public interface ISerializer
    {
        string FolderPath { get; }
        string FilePath { get; }
        string SelectFile(string name);
    }
    public abstract class Serializer : ISerializer
    {
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }
        public abstract string Extension { get; }

        public string SelectFile(string name)
        {
            var name_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{name}.{Extension}");
            if (File.Exists(name_file) == false)
            {
                File.Create(name_file).Close();
            }
            FilePath = name_file;
            return FilePath;
        }
    }

}