using System;
using System.Collections.Generic;
using System.Linq;
using Assignment_01.Models;

namespace Assignment_01.Services
{
    // LINQ - Transformation Operators (Select, SelectMany, anonymous/tuple projections)
    public class TransformationQueries
    {
        private readonly List<Product> _products;
        private readonly List<Customer> _customers;

        public TransformationQueries(List<Product> products, List<Customer> customers)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products));
            _customers = customers ?? throw new ArgumentNullException(nameof(customers));
        }

        // 1) Just the names of the products.
        public IEnumerable<string> ProductNames()
            => _products.Select(product => product.ProductName);

        // 2) Uppercase and lowercase versions of every word (projection).
        public IEnumerable<(string Upper, string Lower)> UpperAndLowerCaseWords()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            return words.Select(word => (Upper: word.ToUpperInvariant(), Lower: word.ToLowerInvariant()));
        }

        // 3) Some product properties, with UnitPrice renamed to Price.
        public IEnumerable<(long ProductID, string ProductName, string Category, decimal Price)> ProductSummaries()
            => _products.Select(product => (
                product.ProductID,
                product.ProductName,
                product.Category,
                Price: product.UnitPrice));

        // 4) Whether each number matches its position in the array.
        public IEnumerable<(int Number, bool InPlace)> NumbersInPlace()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            return numbers.Select((number, index) => (Number: number, InPlace: number == index));
        }

        // 5) All pairs where the number from numbersA is less than the number from numbersB.
        public IEnumerable<string> PairsWhereALessThanB()
        {
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            return from a in numbersA
                   from b in numbersB
                   where a < b
                   select $"{a} is less than {b}";
        }

        // 6) All orders whose total is less than 500.00.
        public IEnumerable<Order> OrdersBelowFiveHundred()
            => _customers.SelectMany(customer => customer.Orders)
                         .Where(order => order.Total < 500m);

        // 7) All orders made in 1998 or later.
        public IEnumerable<Order> OrdersFrom1998OrLater()
            => from customer in _customers
               from order in customer.Orders
               where order.OrderDate.Year >= 1998
               select order;
    }
}
