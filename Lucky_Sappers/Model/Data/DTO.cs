using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    [Serializable]
    public class DTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Counter { get; set; }
        public List<Kletka> kletkas { get; set; }

    }
    [Serializable]
    public class Kletka
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public string Type {  get; set; }
    }
}
