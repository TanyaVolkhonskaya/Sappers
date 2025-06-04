using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class Kletka : ISvoystva
    {
        public bool IsBomb { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsDigit { get; set; }
        public bool Empty { get; set; }
        public bool Openspases { get; set; }
        public int Counter { get; set; }
        public int CountFlag { get; set; }
        public int Level { get; set; }
        
    }
    public class Bomb : Kletka
    {
        public  Bomb()
        {
            IsBomb = true;
        }
    }
    public class Flag : Kletka 
    {
        public Kletka Original { get; set; }
    }
    public class Digit : Kletka { }
    public class Empty : Kletka { }
}

