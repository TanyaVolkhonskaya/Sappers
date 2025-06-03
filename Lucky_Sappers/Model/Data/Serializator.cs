
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

namespace Model.Core
{
    delegate string Filnames(string filname);
    public interface ISer
    {
        string FolderPath {  get; }
        string FilePath { get; }
        string SelectFile(string path);
    }
    public abstract class Serializer:ISer
    {
        //private static int count;
        public string FolderPath { get; private set; }
        public string FilePath { get;private set; }
       
        protected abstract string Extension { get; }

        public string SelectFile(string path)
        {
            string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string FilePath = Path.Combine(FolderPath,$"{path}.{Extension}");
           if (!File.Exists(FilePath))
            {
                using (File.Create(FilePath)) ;
            }
           return FilePath;
            
        }

        public abstract void Serialize<T>(T obj, string fileName) where T : Sizes;
        public abstract T Deserialize<T>(string fileName) where T : Sizes;
        
    }
}