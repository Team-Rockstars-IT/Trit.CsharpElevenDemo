namespace Trit.DemoConsole._5_Required_properties;

public static class Demo
{
    public static Task Main()
    {
        var banana = new Banana
        {
            Color = "blue",
            IsRipe = false
        };

        // Required member 'Demo.Banana.Color' must be set in the object initializer or attribute constructor.
        // Banana otherBanana = new Banana();

        WriteLine($"{banana.Color} bananas are real");

        return Task.CompletedTask;
    }

    public class Banana
    {
        // FEATURE: Required Properties
        public required string Color { get; init; }

        public required bool? IsRipe { get; init; }

        public string? CountryOfOrigin { get; init; }
    }
}