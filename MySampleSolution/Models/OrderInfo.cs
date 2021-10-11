using System;
namespace MySampleSolution.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        /// Initialzes a new instance of order info <see cref="OrderInfo"/>
        /// </summary>
        /// <param name="orderDate">The date at which the order was received</param>
        /// <param name="model">The model name of the order</param>
        /// <param name="price">The price of the model that is being sold</param>
        /// <param name="quantity">The quantity of the model sold on the order date</param>
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
        public decimal Sales => Price * Quantity;

        public override string ToString()
        {
            //shorthand for ToString("0.00") to place .00 after decimals
            return $"{OrderDate.ToShortDateString()} {Model} ${Price:#,##0.00} {Quantity} ${Sales:#,##0.00}"; 
        }

    }
}
