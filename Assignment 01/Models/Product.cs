using System;

namespace Assignment_01.Models
{
    public class Product : IComparable<Product>
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public int CompareTo(Product? other)
            => other is null ? 1 : UnitPrice.CompareTo(other.UnitPrice);

        public override string ToString()
            => $"[{ProductID}] {ProductName} | {Category} | UnitPrice: {UnitPrice:N2} | UnitsInStock: {UnitsInStock}";
    }
}
