using System;
using System.Collections.Generic;
using System.Linq;
using MasterCardAssignment.Business;
using MasterCardAssignment.Models;
using Xunit;

namespace MasterCardAssignmentTests
{
    public class OrderResultsBusinessTests
    {
        private readonly OrderResultsBusiness _business;
        private IEnumerable<OrderInfo> _orderInfos;

        public OrderResultsBusinessTests()
        {
            _business = new OrderResultsBusiness();
            _orderInfos = new List<OrderInfo>
            {
                new OrderInfo(Convert.ToDateTime("2020-02-28"), "iPhone", 799, 3),
                new OrderInfo(Convert.ToDateTime("2021-07-12"), "iPhone", 899, 5),
                new OrderInfo(Convert.ToDateTime("2021-02-17"), "Galaxy", 799, 3),
                new OrderInfo(Convert.ToDateTime("2021-11-05"), "Galaxy", 699, 2),
                new OrderInfo(Convert.ToDateTime("2020-12-25"), "Pixel", 899, 4),
            };
        }

        [Fact]
        public void SorOrdersByDateShouldThrowArgumentNullExceptionIfNullOrders()
        {
            _orderInfos = null;
            Assert.Throws<ArgumentNullException>(() => _business.SorOrdersByDate(_orderInfos));
        }

        [Fact]
        public void SorOrdersByDateShouldSortOrdersByDate()
        {
            var results = _business.SorOrdersByDate(_orderInfos);
            Assert.Equal(Convert.ToDateTime("2021-11-05"), results.First().OrderDate);
            Assert.Equal(Convert.ToDateTime("2020-02-28"), results.Last().OrderDate);
        }

        [Fact]
        public void GetSalesByModelShouldThrowArgumentNullExceptionIfNullOrders()
        {
            _orderInfos = null;
            Assert.Throws<ArgumentNullException>(() => _business.GetSalesByModel(_orderInfos));
        }

        [Fact]
        public void GetSalesByModelShouldSortHighestGrossingSalesFirst()
        {
            var results = _business.GetSalesByModel(_orderInfos);
            var iPhoneSales = results.First();
            Assert.Equal(iPhoneSales.Value, (799 * 3) + (899 * 5));

            var pixelSales = results.Last();
            Assert.Equal(pixelSales.Value, (899 * 4));

            var galaxySales = results.FirstOrDefault(x => x.Key == "Galaxy");
            Assert.Equal(galaxySales.Value, (799 * 3) + (699 * 2));
        }

        [Fact]
        public void SortOrdersByYearThenPriceShouldThrowArgumentNullExceptionIfNullOrders()
        {
            _orderInfos = null;
            Assert.Throws<ArgumentNullException>(() => _business.SortOrdersByYearThenPrice(_orderInfos));
        }

        [Fact]
        public void SortOrdersByYearThenPriceShouldSortAccordingly()
        {
            var result = _business.SortOrdersByYearThenPrice(_orderInfos);
            Assert.Equal(2020, result.First().OrderDate.Year);
            Assert.Equal(899, result.First().Price);

            Assert.Equal(2021, result.Last().OrderDate.Year);
            Assert.Equal(699, result.Last().Price);
        }
    }
}
