using System;
using System.Collections.Generic;
using System.Linq;
using Assignment_01.Models;

namespace Assignment_01.Services
{
    // LINQ - Aggregate Operators (Count, Sum, Min, Max, Average, GroupBy)
    public class AggregateQueries
    {
        private readonly List<Product> _products;
        private readonly List<Customer> _customers;
        private readonly string[] _words;

        public AggregateQueries(List<Product> products, List<Customer> customers, string[] words)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products));
            _customers = customers ?? throw new ArgumentNullException(nameof(customers));
            _words = words ?? throw new ArgumentNullException(nameof(words));
        }

        // 1) Number of odd numbers in the array.
        public int CountOddNumbers()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            return numbers.Count(number => number % 2 != 0);
        }

        // 2) Every customer and how many orders that customer has.
        public IEnumerable<(string CustomerName, int OrderCount)> CustomerOrderCounts()
            => from customer in _customers
               select (CustomerName: customer.CustomerName, OrderCount: customer.Orders.Length);

        // 3) Every category and how many products it contains.
        public IEnumerable<(string Category, int ProductCount)> CategoryProductCounts()
            => from product in _products
               group product by product.Category into category
               orderby category.Key
               select (Category: category.Key, ProductCount: category.Count());

        // 4) Total of the numbers in the array.
        public int SumOfNumbers()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            return numbers.Sum();
        }

        // 5) Total number of characters of all words in dictionary_english.txt.
        public int TotalCharactersOfWords()
            => _words.Sum(word => word.Length);

        // 6) Length of the shortest word in dictionary_english.txt.
        public int ShortestWordLength()
            => _words.Min(word => word.Length);

        // 7) Length of the longest word in dictionary_english.txt.
        public int LongestWordLength()
            => _words.Max(word => word.Length);

        // 8) Average length of the words in dictionary_english.txt.
        public double AverageWordLength()
            => _words.Average(word => word.Length);
    }
}
