using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class Kletka : ISvoystva
    {
        public bool IsBomb { get; set; }//бомба
        public bool IsFlagged { get; set; }//флаг
        public bool IsDigit { get; set; }//число
        public bool Empty { get; set; }//пустая
        public bool Openspases { get; set; }//открытая
        
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

