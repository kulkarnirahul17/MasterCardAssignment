using System.Collections;
using MasterCardAssignment.Models;

namespace MasterCardAssignment.Business.Comparers
{
    /// <summary>
    /// Compares the orders in reverse order of order date     
    /// </summary>
    /// <remarks>This takes an object because it is used in a generic custom sort funciton</remarks>
    public class OrderDateReverseComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            //compare x > y sorts ascending in the original array 
            //compare y > x will give reverse order
            return
              ((OrderInfo)y).OrderDate.CompareTo(((OrderInfo)x).OrderDate);
        }
    }
}