namespace Trit.DemoConsole._5_Required_properties;

public static class Demo
{
    public static Task Main()
    {
        Banana banana = new Banana
        {
            Color = "blue",
            IsRipe = false
        };

        // [CS9035] Required member 'Demo.Banana.Color' must be set in the object initializer or attribute constructor.
        // [CS9035] Required member 'Demo.Banana.IsRipe' must be set in the object initializer or attribute constructor.
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