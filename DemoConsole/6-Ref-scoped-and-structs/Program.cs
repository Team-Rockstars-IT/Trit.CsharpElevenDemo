namespace Trit.DemoConsole._6_Ref_scoped_and_structs;
#pragma warning disable CS0169

public static class Demo
{
    public static Task Main()
    {
        var team = new PadelTeam(ref Pro, ref Pro, ref Pro, ref Pro);
        WriteLine($"Does the team have a win count? {team.HasWinCount()}");

        Span<int> winCounts = stackalloc int[] { 42, 40, 36, 32 };
        team.SetWinCountToFirstValue(winCounts);

        WriteLine($"How about now? {team.HasWinCount()}");

        team.SetReserve(ref Pro);
        WriteLine($"Who's the reserve? {team.GetNameOfReserve()}");

        return Task.CompletedTask;
    }

    internal ref struct PadelTeam
    {
        // FEATURE: ref fields
        private readonly ref readonly PadelPlayer first;
        private readonly ref readonly PadelPlayer second;
        private readonly ref readonly PadelPlayer third;
        private readonly ref readonly PadelPlayer fourth;
        private ref readonly PadelPlayer reserve;
        private readonly ref int wins;

        // FEATURE: Auto-default structs
        private int ranking;

        public PadelTeam(ref PadelPlayer first, ref PadelPlayer second, ref PadelPlayer third, ref PadelPlayer fourth)
        {
            this.first = ref first;
            this.second = ref second;
            this.third = ref third;
            this.fourth = ref fourth;
        }

        public void SetReserve(ref PadelPlayer newReserve)
        {
            reserve = ref newReserve;
        }

        public string GetNameOfReserve() => $"{reserve.FirstName} {reserve.LastName}";

        // FEATURE: scoped modifier
        // Scoped is required here as promise that we won't attempt to store the reference outside the scope of this method,
        // this allows for a stack-allocated Span<> to be passed in safely
        public void SetWinCountToFirstValue(scoped Span<int> newScore)
        {
            try
            {
                // We can't assign a new value to a reference, if the reference is "null"
                // And since "wins" is readonly in the sense that it can't be repointed, we're now stuck
                // If we were to allocate a reference for "wins" in the constructor, this would work just fine
                wins = newScore[0];
            }
            catch (NullReferenceException)
            {
                WriteLine($"Unable to set win count since there's no ref to mutate");
            }
        }

        public bool HasWinCount() => System.Runtime.CompilerServices.Unsafe.IsNullRef(ref wins);
    }

    public readonly record struct PadelPlayer(string FirstName, string LastName, int BirthYear);

    private static PadelPlayer Pro = new()
    {
        FirstName = "Ale",
        LastName = "Galán",
        BirthYear = 1996
    };
}