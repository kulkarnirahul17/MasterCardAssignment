using System;
namespace MasterCardAssignment.Models
{
    public class OrderInfo
    {
        public OrderInfo(DateTime orderDate, string model, decimal price, int quantity)
        {
            OrderDate = orderDate;
            Model = model;
            Quantity = quantity;
            Price = price;
        }

        public DateTime OrderDate { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Sales => (Price * Quantity);

    }
}
