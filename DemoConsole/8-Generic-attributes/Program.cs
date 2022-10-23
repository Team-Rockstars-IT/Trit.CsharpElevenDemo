namespace Trit.DemoConsole._8_Generic_attributes;

public static class Demo
{
    // FEATURE: Allow Generic Attributes
    [Subclass<SoftwareEngineer>(discriminator: "ENG")]
    public record Person(string FirstName, string LastName, int Age);

    public record SoftwareEngineer(string FirstName, string LastName, int Age)
        : Person(FirstName, LastName, Age);

    public static Task Main()
    {
        var element = JsonSerializer.Deserialize<JsonElement>(
            PersonJson,
            CamelOptions);

        var discriminator = element.EnumerateObject()
            .First(o => o.Name == "type")
            .Value.GetString();
        Type subclass = GetSubclassTypeFromDiscriminator<Person>(discriminator);

        var programmer = element.Deserialize(subclass, CamelOptions) as SoftwareEngineer;
        WriteLine($"Deserialized engineer with first name: {programmer?.FirstName}");

        return Task.CompletedTask;
    }

    #region Not interesting

    private static Type GetSubclassTypeFromDiscriminator<T>(string? discriminator)
    {
        // Find the SubclassAttribute matching the given discriminator
        // and get its generic type parameter
        Type? subClassType = typeof(T)
            .GetCustomAttributes(inherit: true)
            .FirstOrDefault(a =>
                a.GetType().IsGenericType
                && a.GetType().GetGenericTypeDefinition() == typeof(SubclassAttribute<>)
                && string.Equals(((ISubclassAttribute)a).Discriminator, discriminator, StringComparison.OrdinalIgnoreCase))
            ?.GetType().GenericTypeArguments[0];

        return subClassType
               ?? throw new InvalidOperationException($"Unable to find subclass for: {discriminator}");
    }

    private const string PersonJson = """
        {
            "type": "ENG",
            "firstName": "Rasmus",
            "lastName": "Lerdorf",
            "age": 53
        }
    """;

    public class SubclassAttribute<TSubClass> : Attribute, ISubclassAttribute
    {
        public SubclassAttribute(string discriminator)
        {
            Discriminator = discriminator;
        }

        public string Discriminator { get; }
    }

    public interface ISubclassAttribute
    {
        string Discriminator { get; }
    }

    private static JsonSerializerOptions CamelOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #endregion
}