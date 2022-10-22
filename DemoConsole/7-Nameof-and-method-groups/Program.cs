using System.Runtime.CompilerServices;

namespace Trit.DemoConsole._7_Nameof_and_method_groups;

public static class Demo
{
    public static Task Main()
    {
        AssertIsPositive(10 / 20 - 2);

        // FEATURE: Cache delegates for static method group
        Func<double, double> a = Math.Round;
        Func<double, double> b = Math.Round;

        WriteLine("Do static method group conversions get cached? " +
                  (ReferenceEquals(a, b) ? "Yes" : "No"));

        return Task.CompletedTask;
    }

    // FEATURE: Support for method parameter names in nameof()
    [ParameterIsPotentiallyNegative(nameof(someValue))]
    private static void AssertIsPositive(int someValue,
        [CallerArgumentExpression(nameof(someValue))]
        string expression = "")
    {
        if (someValue < 0)
        {
            WriteLine($"{expression} was found to be negative!");
        }
    }

    private class ParameterIsPotentiallyNegativeAttribute : Attribute
    {
        public ParameterIsPotentiallyNegativeAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}