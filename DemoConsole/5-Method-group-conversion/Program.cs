namespace Trit.DemoConsole._5_Method_group_conversion;

public static class Demo
{
    public static Task Main()
    {
        Func<double, double> a = Math.Round;
        Func<double, double> b = Math.Round;

        WriteLine($"Do static method group conversions get cached? " +
                  $"{ReferenceEquals(a, b)}");

        return Task.CompletedTask;
    }
}