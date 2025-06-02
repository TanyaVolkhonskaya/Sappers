using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ISvoystva
    {
        bool IsBomb { get; } //бомба
        bool IsFlagged { get; set; }// флажок
        bool IsDigit { get; set; }// клетка с числом
        bool Empty { get; set; }// пустая клетка без числа (0)
    }
    

}

