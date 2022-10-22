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

        WriteLine("And do we have a match? " +
                  (IsMatch("hello") ? "Yes" : "No"));

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

    private static bool IsMatch(ReadOnlySpan<char> subject)
    {
        // FEATURE: Switching ReadOnlySpan<char> with constant strings
        return subject switch
        {
            "hello world" => true,
            "hello" => true,
            "world" => true,
            _ => false
        };
    }
}