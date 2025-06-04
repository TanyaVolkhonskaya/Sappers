using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ISvoystva
    {
        bool IsBomb {  get; }
        bool Openspases { get; }
        bool IsFlagged { get; }
        int Counter {  get; }
    }
    

}

