using Newtonsoft.Json;
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
        public bool Empty { get;  set; }//пустая
        public bool Openspases { get;  set; }//открытая
        
    }
    [JsonObject]
    public class Bomb : Kletka
    {
        public  Bomb()
        {
            IsBomb = true;
        }
    }
    [JsonObject]
    public class Flag : Kletka 
    { public Flag() { } }
    [JsonObject]
    public class Digit : Kletka 
    {
        public Digit() { }
    }
    [JsonObject]
    public class Empty : Kletka 
    {
        public Empty() { }
    }
}

