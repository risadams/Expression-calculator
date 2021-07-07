using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ExpressionCalculator
{
  public class Program
  {
    private static void Main()
    {
      Console.WriteLine(Calculator.Calculate("12.2"));
      Console.WriteLine(Calculator.Calculate("10 PLUS 5"));
      Console.WriteLine(Calculator.Calculate("10 + 2 * 5"));
      Console.WriteLine(Calculator.Calculate("100 * 2 - 50"));
      Console.WriteLine(Calculator.Calculate("5 * 2 ^ 2 / 4"));
      Console.WriteLine(Calculator.Calculate("107.485156"));
      Console.WriteLine(Calculator.Calculate("12.2 * 7.34 + 9"));
      Console.WriteLine(Calculator.Calculate("9 MOD 6"));
      Console.WriteLine(Calculator.Calculate("9 % 6"));
    }
  }

  public static class Calculator
  {
    private static double _result;
    private static double? _currentValue;
    private static string _currentOperation;
    private static bool _first;
    private static readonly Queue<string> SymQueue = new Queue<string>();

    public static readonly IDictionary<string, Func<double, double, double>> Operators =
      new Dictionary<string, Func<double, double, double>>
      {
        { "+", (x, y) => x + y },
        { "PLUS", (x, y) => x + y },
        { "-", (x, y) => x - y },
        { "*", (x, y) => x * y },
        {
          "/", (x, y) =>
          {
            if (Math.Abs(y - 0) < 0.001) throw new DivideByZeroException();
            return x / y;
          }
        },
        { "MOD", (x, y) => x % y },
        { "%", (x, y) => x % y },
        { "^", Math.Pow }
      };

    public static double Calculate(string input)
    {
      _result           = 0d;
      _currentValue     = null;
      _currentOperation = null;
      _first            = true;

      var symbols = Regex.Split(input, "\\s+", RegexOptions.Compiled | RegexOptions.Singleline);
      foreach (var symbol in symbols) SymQueue.Enqueue(symbol);

      switch (SymQueue.Count)
      {
        case 0:
          throw new Exception("Unable to parse input string");
        case 1 when double.TryParse(SymQueue.Dequeue(), NumberStyles.Float, CultureInfo.InvariantCulture, out var d):
          return d;
        default:
          InternalCalculateWithLoop(SymQueue);
          return _result;
      }
    }

    private static void InternalCalculateWithRecursion(Queue<string> symQ)
    {
      if (symQ.Count == 0) return;

      var temp     = symQ.Dequeue();
      var isDouble = double.TryParse(temp, NumberStyles.Float, CultureInfo.InvariantCulture, out var d);
      if (isDouble)
      {
        _currentValue = d;
      }

      else
      {
        if (!Operators.ContainsKey(temp ?? string.Empty))
          throw new Exception(string.Format(CultureInfo.InvariantCulture, "Unrecognized symbol {0} or unable to parse value as a double", temp));
        _currentOperation = temp;
      }

      if (!string.IsNullOrEmpty(_currentOperation) && _currentValue.HasValue)
      {
        if (_first)
        {
          _result = _currentValue.Value;
          _first  = false;
        }
        else
        {
          _result           = Operators[_currentOperation](_result, _currentValue.Value);
          _currentValue     = null;
          _currentOperation = null;
        }
      }

      InternalCalculateWithRecursion(symQ);
    }

    private static void InternalCalculateWithLoop(Queue<string> symQ)
    {
      while (true)
      {
        if (symQ.Count == 0) return;

        var temp     = symQ.Dequeue();
        var isDouble = double.TryParse(temp, NumberStyles.Float, CultureInfo.InvariantCulture, out var d);
        if (isDouble)
        {
          _currentValue = d;
        }

        else
        {
          if (!Operators.ContainsKey(temp ?? string.Empty)) throw new Exception(string.Format(CultureInfo.InvariantCulture, "Unrecognized symbol {0} or unable to parse value as a double", temp));
          _currentOperation = temp;
        }

        if (!string.IsNullOrEmpty(_currentOperation) && _currentValue.HasValue)
        {
          if (_first)
          {
            _result = _currentValue.Value;
            _first  = false;
          }
          else
          {
            _result           = Operators[_currentOperation](_result, _currentValue.Value);
            _currentValue     = null;
            _currentOperation = null;
          }
        }
      }
    }
  }
}
