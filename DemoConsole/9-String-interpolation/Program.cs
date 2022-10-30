namespace Trit.DemoConsole._9_String_interpolation;

public static class Demo
{
    public static Task Main()
    {
        Person person = GetPerson();

        // FEATURE: Newlines in interpolation
        WriteLine(
            $"The colors of the flag of the country, " +
            $"of the inventor of the flag " +
            $"of the country where Mark is from are: {string.Join(", ",
                person
                    .HouseAddress
                    .Country
                    .Flag
                    .FlagInventor
                    .HouseAddress
                    .Country.Flag.Colors
                ?? Array.Empty<string>())}");

        return Task.CompletedTask;
    }

    private static Person GetPerson()
    {
        return new Person
        {
            HouseAddress = new Address
            {
                Country = new Country
                {
                    Flag = new Flag
                    {
                        FlagInventor = new Person
                        {
                            HouseAddress = new Address
                            {
                                Country = new Country
                                {
                                    Flag = new Flag
                                    {
                                        Colors = new[]
                                        {
                                            "#A91F32",
                                            "#FFFFFF",
                                            "#1E4785"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    public class Person
    {
        public Address HouseAddress { get; init; } = null!;
    }

    public class Address
    {
        public Country Country { get; init; } = null!;
    }

    public class Country
    {
        public Flag Flag { get; init; } = null!;
    }

    public class Flag
    {
        public Person FlagInventor { get; init; } = null!;

        public string[]? Colors { get; set; }
    }
}