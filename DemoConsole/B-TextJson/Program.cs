using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Trit.DemoConsole.B_TextJson;

public static class Demo
{
    public static Task Main()
    {
        var serializerOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = new CustomTypeInfoResolver()
        };

        var serializedPort = JsonSerializer.Serialize<ComputerPort>(
            new USBPort(Address: @"PCI\VEN_1022&DEV_149C"),
            serializerOptions);

        WriteLine(serializedPort);

        var deserializedPort = JsonSerializer.Deserialize<ComputerPort>(
            serializedPort,
            serializerOptions);
        WriteLine(deserializedPort);

        return Task.CompletedTask;
    }

    // FEATURE: JsonSerializer polymorphic serialization
    [JsonDerivedType(typeof(USBPort), "usb")]
    public record ComputerPort(string Address);

    public record USBPort(string Address) : ComputerPort(Address);

    // FEATURE: JSON contract customization
    public class CustomTypeInfoResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(
            Type type,
            JsonSerializerOptions options)
        {
            JsonTypeInfo typeInfo = base.GetTypeInfo(type, options);

            if (typeInfo.Type == typeof(USBPort))
            {
                JsonPropertyInfo addressProperty = typeInfo.Properties
                    .First(p => p.Name == nameof(USBPort.Address));

                addressProperty.Name = "HardwareId";
            }

            return typeInfo;
        }
    }
}