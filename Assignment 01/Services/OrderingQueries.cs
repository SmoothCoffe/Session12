using System;
using System.Collections.Generic;
using System.Linq;
using Assignment_01.Comparers;
using Assignment_01.Models;

namespace Assignment_01.Services
{
    // LINQ - Ordering Operators (OrderBy, OrderByDescending, ThenBy, ThenByDescending)
    public class OrderingQueries
    {
        private readonly List<Product> _products;

        public OrderingQueries(List<Product> products)
            => _products = products ?? throw new ArgumentNullException(nameof(products));

        // 1) Sort products by name.
        public IEnumerable<Product> ProductsByName()
            => _products.OrderBy(product => product.ProductName);

        // 2) Case-insensitive sort using a custom comparer.
        public IEnumerable<string> CaseInsensitiveSortedWords()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            return words.OrderBy(word => word, new CaseInsensitiveComparer());
        }

        // 3) Products by units in stock, highest first.
        public IEnumerable<Product> ProductsByStockDescending()
            => _products.OrderByDescending(product => product.UnitsInStock);

        // 4) Digits by name length, then alphabetically by name.
        public IEnumerable<string> DigitsByLengthThenAlphabetically()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            return digits.OrderBy(digit => digit.Length)
                         .ThenBy(digit => digit);
        }

        // 5) Words by length, then case-insensitive alphabetical order.
        public IEnumerable<string> WordsByLengthThenCaseInsensitive()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            return words.OrderBy(word => word.Length)
                        .ThenBy(word => word, StringComparer.OrdinalIgnoreCase);
        }

        // 6) Products by category, then by unit price from highest to lowest.
        public IEnumerable<Product> ProductsByCategoryThenPriceDescending()
            => _products.OrderBy(product => product.Category)
                        .ThenByDescending(product => product.UnitPrice);

        // 7) Words by length, then case-insensitive descending order.
        public IEnumerable<string> WordsByLengthThenCaseInsensitiveDescending()
        {
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            return words.OrderBy(word => word.Length)
                        .ThenByDescending(word => word, StringComparer.OrdinalIgnoreCase);
        }

        // 8) Digits whose second letter is 'i', reversed from the original order.
        public IEnumerable<string> DigitsWithSecondLetterIReversed()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            return digits.Where(digit => digit.Length > 1 && digit[1] == 'i')
                         .Reverse();
        }
    }
}
