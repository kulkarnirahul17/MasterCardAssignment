using System;
using System.Collections.Generic;

namespace MasterCardAssignment.Business.Comparers
{
    public class OrderModelSalesPairComparator : IComparer<KeyValuePair<string, decimal>>
    {     
        public int Compare(KeyValuePair<string, decimal> x, KeyValuePair<string, decimal> y)
        {
            //comparing y first sorts the results in decreasing order
            return y.Value.CompareTo(x.Value);
        }
    }
}
