# Assignment 01 - LINQ (C#)

A console application that solves all LINQ exercises in **Assignment 01** using the
provided `ListGenerator.cs`, `Customers.xml` and `dictionary_english.txt` files.

The solution is organised in the same OOP style as the Session 12 examples:

- Models (`Product`, `Customer`, `Order`)
- A data layer (`ListGenerator`, `DataFiles`)
- One query service per LINQ category
- A `Program` class that runs the queries and prints the results

---

## Project structure

```
Assignment 01 Solution/
├── Assignment 01.slnx
└── Assignment 01/
    ├── Assignment 01.csproj
    ├── Program.cs
    ├── Customers.xml
    ├── dictionary_english.txt
    ├── Models/
    │   ├── Product.cs
    │   ├── Customer.cs
    │   └── Order.cs
    ├── Data/
    │   ├── ListGenerator.cs
    │   └── DataFiles.cs
    ├── Comparers/
    │   └── CaseInsensitiveComparer.cs
    └── Services/
        ├── RestrictionQueries.cs
        ├── ElementQueries.cs
        ├── AggregateQueries.cs
        ├── OrderingQueries.cs
        └── TransformationQueries.cs
```

## How to run

Open `Assignment 01.slnx` in Visual Studio and press **F5**, or run:

```bash
dotnet run --project "Assignment 01/Assignment 01.csproj"
```

`Customers.xml` and `dictionary_english.txt` are copied next to the executable by the
`.csproj`, and `DataFiles.Resolve` also falls back to the current directory, so the data
loads correctly from Visual Studio as well.

---

## Query coverage

### Restriction Operators (`Where`)

| # | Query | Implementation |
|---|---|---|
| 1 | Products out of stock | `Where(p => p.UnitsInStock == 0)` |
| 2 | In stock and cost more than 3.00 | `Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m)` |
| 3 | Digits whose name is shorter than their value | `digits.Where((name, value) => name.Length < value)` |

### Element Operators (`FirstOrDefault`, `ElementAt`)

| # | Query | Implementation |
|---|---|---|
| 1 | First product out of stock | `FirstOrDefault(p => p.UnitsInStock == 0)` |
| 2 | First product with price > 1000, otherwise null | `FirstOrDefault(p => p.UnitPrice > 1000m)` |
| 3 | Second number greater than 5 | `Where(n => n > 5).ElementAt(1)` |

### Aggregate Operators

| # | Query | Implementation |
|---|---|---|
| 1 | Number of odd numbers | `Count(n => n % 2 != 0)` |
| 2 | Customers and their order counts | `from c in customers select (c.CustomerName, c.Orders.Length)` |
| 3 | Categories and their product counts | `group product by product.Category into category` |
| 4 | Total of the numbers | `Sum()` |
| 5 | Total characters of all dictionary words | `Sum(word => word.Length)` |
| 6 | Shortest word length | `Min(word => word.Length)` |
| 7 | Longest word length | `Max(word => word.Length)` |
| 8 | Average word length | `Average(word => word.Length)` |

### Ordering Operators

| # | Query | Implementation |
|---|---|---|
| 1 | Products by name | `OrderBy(p => p.ProductName)` |
| 2 | Case-insensitive sort with a custom comparer | `OrderBy(w => w, new CaseInsensitiveComparer())` |
| 3 | Products by stock, highest first | `OrderByDescending(p => p.UnitsInStock)` |
| 4 | Digits by name length, then alphabetically | `OrderBy(d => d.Length).ThenBy(d => d)` |
| 5 | Words by length, then case-insensitive | `OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase)` |
| 6 | Products by category, then price descending | `OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice)` |
| 7 | Words by length, then case-insensitive descending | `OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase)` |
| 8 | Digits whose second letter is `i`, reversed | `Where(d => d[1] == 'i').Reverse()` |

### Transformation Operators

| # | Query | Implementation |
|---|---|---|
| 1 | Product names only | `Select(p => p.ProductName)` |
| 2 | Uppercase and lowercase versions | `Select(w => (Upper: w.ToUpperInvariant(), Lower: w.ToLowerInvariant()))` |
| 3 | Product properties with `UnitPrice` renamed to `Price` | tuple projection with `Price: p.UnitPrice` |
| 4 | Whether each number matches its position | `Select((number, index) => (Number: number, InPlace: number == index))` |
| 5 | Pairs where `a < b` | `from a in numbersA from b in numbersB where a < b select ...` |
| 6 | Orders with total less than 500.00 | `SelectMany(c => c.Orders).Where(o => o.Total < 500m)` |
| 7 | Orders made in 1998 or later | query syntax `from c in customers from o in c.Orders where o.OrderDate.Year >= 1998` |

---

## Notes

- **Two LINQ syntaxes** are demonstrated: fluent/method syntax for most queries and query
  syntax for grouping, pairs, and 1998+ orders.
- The query services return `IEnumerable<T>`, so most queries use **deferred execution**
  and run only when `Program` enumerates them. `Count`, `Sum`, `Min`, `Max`, `Average` and
  `ElementAt` are **immediate** (eager) operators.
- A custom `CaseInsensitiveComparer` is used for the "custom comparer" exercise; the other
  case-insensitive sorts use `StringComparer.OrdinalIgnoreCase`.
- The dictionary file is read once in `Program` and passed to `AggregateQueries`, so the
  four dictionary-related queries do not re-read the file.
- `Customers.xml` is loaded with `XDocument` exactly like the provided `ListGenerator.cs`.

## Expected key results (from the assignment screenshots)

- Numbers in place: only `3`, `6` and `7` are `True`.
- Pairs where `a < b` start with `0 is less than 1`, `0 is less than 3`, ...
- Digits with second letter `i` reversed: `nine`, `eight`, `six`, `five`.
