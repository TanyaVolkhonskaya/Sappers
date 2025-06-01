using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky_Sappers
{
    public interface ISvoystva
    {
        bool IsBomb { get; } //бомба
        bool IsFlagged { get; set; }// флажок
        bool IsDigit { get; set; }// клетка с числом
        int Counter { get; set; }// счётчик мин
    }
    

}

