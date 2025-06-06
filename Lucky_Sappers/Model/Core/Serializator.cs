
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model.Core
{
    public abstract class Serializer
    {
        //private static int count;
        public string FilePath { get; private set; }
        public string FolderPath
        {
            get
            {
                string baseDir = AppContext.BaseDirectory;
                string projectDir = Path.GetFullPath(Path.Combine(baseDir, "../../.."));
                string dataDir = Path.Combine(projectDir, "Data");
                if (!Directory.Exists(dataDir))
                    Directory.CreateDirectory(dataDir);
                return dataDir;
            }
        }
        protected abstract string Extension { get; }

        public void SelectFile(string name)
        {
            string fileName = String.Concat(name, ".", Extension);
            FilePath = Path.Combine(FolderPath, fileName);
            string[] file = Directory.GetFiles(FolderPath, fileName);
            if (file == null || file.Length == 0)
                using (File.Create(FilePath)) { }
            ;
        }

        public abstract void Serialize<T>(T obj, string fileName) where T : Sizes;
        public abstract T Deserialize<T>(string fileName) where T : Sizes;
    }
}