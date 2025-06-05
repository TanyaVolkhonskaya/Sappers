
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
    public interface ISerialize
    {
        bool SetFilePath(string filePath);
        void Save(Sizes world);
        void Load(Sizes world);
    }
    public abstract class Serializer : ISerialize
    {
        //private static int count;
        protected string FileP;
        public bool SetFilePath(string filePath)
        {
            if (filePath == null) filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FileP = Path.Combine(filePath, "save");
            return File.Exists(filePath);
        }
        public virtual void Save(Sizes world) { }
        public virtual void Load(Sizes world) { }
    }
}