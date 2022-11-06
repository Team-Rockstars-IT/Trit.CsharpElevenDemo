namespace Trit.DemoConsole.C_Int;

public static class Demo
{
    public static Task Main()
    {
        WriteLine("One light year is:");

        const uint gigameters = 9_460_730;
        WriteLine($"\t{gigameters:N} gigameters");

        const ulong meters = 9_460_730_472_580_800;
        WriteLine($"\t{meters:N} meters");

        // FEATURE: Add support for Int128 and UInt128 data types
        var micrometers = new UInt128(
            upper: 512,
            lower: 15_997_506_841_509_572_608);
        WriteLine($"\t{micrometers:N} micrometers");

        return Task.CompletedTask;
    }
}