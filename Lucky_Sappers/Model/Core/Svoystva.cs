using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ISvoystva
    {
        bool IsBomb { get;  }//бомба
        bool Openspases { get; }//открытая
        bool IsFlagged { get; }//флажковая
    }
    

}

