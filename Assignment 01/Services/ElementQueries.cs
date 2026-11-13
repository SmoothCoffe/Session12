using System;
using System.Collections.Generic;
using System.Linq;
using Assignment_01.Models;

namespace Assignment_01.Services
{
    // LINQ - Element Operators (First, FirstOrDefault, ElementAt)
    public class ElementQueries
    {
        private readonly List<Product> _products;

        public ElementQueries(List<Product> products)
            => _products = products ?? throw new ArgumentNullException(nameof(products));

        // 1) First product out of stock.
        public Product? FirstOutOfStockProduct()
            => _products.FirstOrDefault(p => p.UnitsInStock == 0);

        // 2) First product whose price is greater than 1000,
        //    or null when no such product exists.
        public Product? FirstProductAboveThousand()
            => _products.FirstOrDefault(p => p.UnitPrice > 1000m);

        // 3) Second number greater than 5.
        public int SecondNumberGreaterThanFive()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            return numbers.Where(number => number > 5)
                          .ElementAt(1);
        }
    }
}
