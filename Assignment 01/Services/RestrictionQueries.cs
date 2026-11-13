using System;
using System.Collections.Generic;
using System.Linq;
using Assignment_01.Models;

namespace Assignment_01.Services
{
    // LINQ - Restriction Operators (Where)
    public class RestrictionQueries
    {
        private readonly List<Product> _products;

        public RestrictionQueries(List<Product> products)
            => _products = products ?? throw new ArgumentNullException(nameof(products));

        // 1) All products that are out of stock.
        public IEnumerable<Product> OutOfStockProducts()
            => _products.Where(p => p.UnitsInStock == 0);

        // 2) Products that are in stock and cost more than 3.00 per unit.
        public IEnumerable<Product> InStockAndCostMoreThanThree()
            => _products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m);

        // 3) Digits whose name is shorter than their value.
        //    The index overload of Where supplies the digit value (0 .. 9).
        public IEnumerable<string> DigitsWithNamesShorterThanValue()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            return digits.Where((name, value) => name.Length < value);
        }
    }
}
