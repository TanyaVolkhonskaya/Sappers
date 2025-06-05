
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
        void Save(Sizes siziki);
        Sizes Load(string siziki);
    }
    public abstract class Serializer : ISerialize
    {
        //private static int count;
        protected string FilePath;
        public bool SetFilePath(string filePath)
        {
            if (filePath == null) filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FilePath = Path.Combine(filePath, "save");
            return File.Exists(filePath);
        }
        public virtual void Save(Sizes siziki) { }
        public virtual Sizes Load(string siziki)
        {
            return new Sizes(0, 0, 0, 0);
        }
    }
}