# Expression Calculator

This is an archived copy of a solution to an interview problem that I used to present to new hires.

The problem set:

```csharp
namespace ExpressionCalculator
{
    using System;
    public class Calculator
    {
        /// <summary>
        /// Calculates the result of the given <paramref name="expression"/>.
        /// </summary>
        /// <example>
        ///     <code>
        ///     int result1 = Calculate("17 * 9");
        ///     int result2 = Calculate("15/6");
        ///     int result3 = Calculate("2+ 4");
        ///     </code>
        /// </example>
        /// <remarks>
        ///   1) You do not need to implement the entire solution in one method.
        ///   2) Feel free to run the program as you go to verify your results.
        ///   3) There are many ways of solving this problem - we are not looking
        ///      for anything extravagant. Feel free to ask questions.
        /// </remarks>
        /// <param name="expression">The expression to calculate.</param>
        /// <returns>The result of the calculated expression.</returns>
        public int Calculate(string expression)
        {
            throw new NotImplementedException("Implement");
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            var calculator = new Calculator();

            Console.WriteLine("Enter an expression: ");
            var exp = Console.ReadLine();
            var result = calculator.Calculate(exp);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
```

I was looking for:

* The ability to split a string
* Convert string to a double
* interpret a symbol as an operator (switch statement)
* providing correct output
* basic error handling (eg. divide by zero)

My solution is much more complex than necessary, but also shows basic data structures, recursion, loops, and light expressions.

## Contribute

If you think this could be better, please [open an issue](https://github.com/risadams/Expression-calculator/issues/new)!

Please note that all interactions in this organization fall under our [Code of Conduct](CODE_OF_CONDUCT.md).

## License

[MIT](LICENSE) © 1996+ Ris Adams
