using System;

namespace Assignment_01.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        public override string ToString()
            => $"Order Id: {OrderID}, Date: {OrderDate:yyyy-MM-dd}, Total: {Total:N2}";
    }
}
