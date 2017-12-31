using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrder.Model
{
    public enum OrderType
    {
        DillerGold = 1,
        DillerSilver = 2,        
        Social = 3,
        Retail = 4
    }

    public enum OrderStatus
    {
        Valid = 1,
        Cancelled = 2
    }
}
