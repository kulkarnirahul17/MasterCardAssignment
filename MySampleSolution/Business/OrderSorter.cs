using System;
using System.Collections;
using System.Collections.Generic;
using MySampleSolution.Models;

namespace MySampleSolution.Business
{
    public class OrderSorter
    {
        /// <summary>
        /// Provides a custom order sorting routine using a variation of quick sort
        /// </summary>
        /// <param name="orderInfos">The orders that need to be sorted</param>
        /// <param name="comparer"></param>
        public static void QuickSort(object[] orderInfos, IComparer comparer)
        {            
            quickSort(orderInfos, 0, orderInfos.Length - 1, comparer);
        }

        private static void quickSort(object[] itemsToBeSorted, int left, int right, IComparer comparer)
        {
            if (left >= right)
                return;

            int p = partition(itemsToBeSorted, left, right, comparer);
            quickSort(itemsToBeSorted, left, p - 1, comparer);
            quickSort(itemsToBeSorted, p + 1, right, comparer);
        }

        private static int partition(object[] orderInfos, int left, int right, IComparer comparer)
        {
            object pivot = orderInfos[right];
            int index = left - 1;

            for (int i = left; i <= right - 1; i++)
            {
                if (comparer.Compare(orderInfos[i], pivot) < 0)
                {
                    index++;
                    swap(orderInfos, index, i);
                }
            }
            swap(orderInfos, index + 1, right);
            return index + 1;
        }

        private static void swap(Object[] orderInfos, int index, int i)
        {
            object temp = orderInfos[index];
            orderInfos[index] = orderInfos[i];
            orderInfos[i] = temp;
        }
    }
}
