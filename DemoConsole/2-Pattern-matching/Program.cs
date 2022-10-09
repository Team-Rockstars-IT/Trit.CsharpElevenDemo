namespace Trit.DemoConsole._2_Pattern_matching;

public static class Demo
{
    public static Task Main()
    {
        WriteLine(GetValueAtIndexZero(new List<int>()));
        WriteLine(GetValueAtIndexZero(new List<int> { 0, 1, 2 }));
        WriteLine(GetValueAtIndexZero(new List<int> { 1, 2, 3 }));

        if (new List<int> { 0, 1, 2 } is [.., 2])
        {
            WriteLine("2");
        }

        return Task.CompletedTask;
    }

    private static int GetValueAtIndexZero(IList<int> values)
    {
        // FEATURE: List patterns
        return values switch
        {
            [ 1, .. ] => 1,
            [ var x, .. ] => x,
            { Count: 0 } => -1
        };
    }
}