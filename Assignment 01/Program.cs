using System;
using System.Collections.Generic;
using Assignment_01.Data;
using Assignment_01.Models;
using Assignment_01.Services;

namespace Assignment_01
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Product> products = ListGenerator.ProductsList;
            List<Customer> customers = ListGenerator.CustomersList;
            string[] words = DataFiles.LoadWords("dictionary_english.txt");

            RunRestrictionQueries(new RestrictionQueries(products));
            RunElementQueries(new ElementQueries(products));
            RunAggregateQueries(new AggregateQueries(products, customers, words));
            RunOrderingQueries(new OrderingQueries(products));
            RunTransformationQueries(new TransformationQueries(products, customers));

            Console.WriteLine();
            Console.WriteLine("==================== End of Assignment 01 ====================");
        }

        // ------------------------------------------------------------------
        // LINQ - Restriction Operators
        // ------------------------------------------------------------------
        private static void RunRestrictionQueries(RestrictionQueries queries)
        {
            PrintHeader("LINQ - Restriction Operators");

            Console.WriteLine("1. Products that are out of stock:");
            PrintResults(queries.OutOfStockProducts());

            Console.WriteLine();
            Console.WriteLine("2. Products in stock and cost more than 3.00 per unit:");
            PrintResults(queries.InStockAndCostMoreThanThree());

            Console.WriteLine();
            Console.WriteLine("3. Digits whose name is shorter than their value:");
            PrintResults(queries.DigitsWithNamesShorterThanValue());
        }

        // ------------------------------------------------------------------
        // LINQ - Element Operators
        // ------------------------------------------------------------------
        private static void RunElementQueries(ElementQueries queries)
        {
            PrintHeader("LINQ - Element Operators");

            Console.WriteLine("1. First product out of stock:");
            Product? firstOutOfStock = queries.FirstOutOfStockProduct();
            Console.WriteLine(firstOutOfStock?.ToString() ?? "null");

            Console.WriteLine();
            Console.WriteLine("2. First product whose Price > 1000 (or null):");
            Product? firstAboveThousand = queries.FirstProductAboveThousand();
            Console.WriteLine(firstAboveThousand?.ToString() ?? "null");

            Console.WriteLine();
            Console.WriteLine("3. Second number greater than 5:");
            Console.WriteLine(queries.SecondNumberGreaterThanFive());
        }

        // ------------------------------------------------------------------
        // LINQ - Aggregate Operators
        // ------------------------------------------------------------------
        private static void RunAggregateQueries(AggregateQueries queries)
        {
            PrintHeader("LINQ - Aggregate Operators");

            Console.WriteLine($"1. Number of odd numbers: {queries.CountOddNumbers()}");

            Console.WriteLine();
            Console.WriteLine("2. Customers and how many orders each has:");
            foreach (var (customerName, orderCount) in queries.CustomerOrderCounts())
                Console.WriteLine($"{customerName} -> {orderCount} orders");

            Console.WriteLine();
            Console.WriteLine("3. Categories and how many products each has:");
            foreach (var (category, productCount) in queries.CategoryProductCounts())
                Console.WriteLine($"{category} -> {productCount} products");

            Console.WriteLine();
            Console.WriteLine($"4. Total of the numbers: {queries.SumOfNumbers()}");

            Console.WriteLine();
            Console.WriteLine($"5. Total number of characters in dictionary_english.txt: {queries.TotalCharactersOfWords():N0}");
            Console.WriteLine($"6. Shortest word length: {queries.ShortestWordLength()}");
            Console.WriteLine($"7. Longest word length: {queries.LongestWordLength()}");
            Console.WriteLine($"8. Average word length: {queries.AverageWordLength():F4}");
        }

        // ------------------------------------------------------------------
        // LINQ - Ordering Operators
        // ------------------------------------------------------------------
        private static void RunOrderingQueries(OrderingQueries queries)
        {
            PrintHeader("LINQ - Ordering Operators");

            Console.WriteLine("1. Products sorted by name:");
            PrintResults(queries.ProductsByName());

            Console.WriteLine();
            Console.WriteLine("2. Words sorted case-insensitively using a custom comparer:");
            PrintResults(queries.CaseInsensitiveSortedWords());

            Console.WriteLine();
            Console.WriteLine("3. Products sorted by units in stock (highest to lowest):");
            PrintResults(queries.ProductsByStockDescending());

            Console.WriteLine();
            Console.WriteLine("4. Digits sorted by name length, then alphabetically:");
            PrintResults(queries.DigitsByLengthThenAlphabetically());

            Console.WriteLine();
            Console.WriteLine("5. Words sorted by length, then case-insensitively:");
            PrintResults(queries.WordsByLengthThenCaseInsensitive());

            Console.WriteLine();
            Console.WriteLine("6. Products sorted by category, then unit price (highest to lowest):");
            PrintResults(queries.ProductsByCategoryThenPriceDescending());

            Console.WriteLine();
            Console.WriteLine("7. Words sorted by length, then case-insensitively descending:");
            PrintResults(queries.WordsByLengthThenCaseInsensitiveDescending());

            Console.WriteLine();
            Console.WriteLine("8. Digits whose second letter is 'i', reversed:");
            PrintResults(queries.DigitsWithSecondLetterIReversed());
        }

        // ------------------------------------------------------------------
        // LINQ - Transformation Operators
        // ------------------------------------------------------------------
        private static void RunTransformationQueries(TransformationQueries queries)
        {
            PrintHeader("LINQ - Transformation Operators");

            Console.WriteLine("1. Product names:");
            PrintResults(queries.ProductNames());

            Console.WriteLine();
            Console.WriteLine("2. Uppercase and lowercase versions of each word:");
            foreach (var (upper, lower) in queries.UpperAndLowerCaseWords())
                Console.WriteLine($"Upper: {upper}, Lower: {lower}");

            Console.WriteLine();
            Console.WriteLine("3. Product properties with UnitPrice renamed to Price:");
            foreach (var product in queries.ProductSummaries())
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, " +
                                  $"Category: {product.Category}, Price: {product.Price:N2}");

            Console.WriteLine();
            Console.WriteLine("4. Numbers and whether they are in place:");
            foreach (var (number, inPlace) in queries.NumbersInPlace())
                Console.WriteLine($"Number: {number}, In-place? {inPlace}");

            Console.WriteLine();
            Console.WriteLine("5. Pairs where a < b:");
            PrintResults(queries.PairsWhereALessThanB());

            Console.WriteLine();
            Console.WriteLine("6. Orders with a total less than 500.00:");
            PrintResults(queries.OrdersBelowFiveHundred());

            Console.WriteLine();
            Console.WriteLine("7. Orders made in 1998 or later:");
            PrintResults(queries.OrdersFrom1998OrLater());
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------
        private static void PrintResults<T>(IEnumerable<T> results)
        {
            bool hasResults = false;

            foreach (T result in results)
            {
                Console.WriteLine(result);
                hasResults = true;
            }

            if (!hasResults)
                Console.WriteLine("(no results)");
        }

        private static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 70));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 70));
        }
    }
}
