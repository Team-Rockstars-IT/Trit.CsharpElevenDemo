namespace Trit.DemoConsole._6_Ref_scoped_and_structs;
#pragma warning disable CS0169

public static class Demo
{
    public static Task Main()
    {
        var team = new PadelTeam(first: ref Galan);
        WriteLine($"Does the team have a win count? {team.HasWinCount()}");

        Span<int> winCounts = stackalloc int[] { 42, 40, 36, 32 };
        team.SetWinCountToFirstValue(winCounts);

        WriteLine($"How about now? {team.HasWinCount()}");

        team.SetSecondPlayer(newSecondPlayer: ref Lebron);
        WriteLine($"Who's the second player? {team.GetNameOfSecondPlayer()}");

        return Task.CompletedTask;
    }

    internal ref struct PadelTeam
    {
        // FEATURE: ref fields
        private readonly ref readonly PadelPlayer first;
        private ref readonly PadelPlayer second;
        private ref int wins;

        // FEATURE: Auto-default structs
        private int ranking;

        public PadelTeam(ref PadelPlayer first)
        {
            this.first = ref first;
        }

        public void SetSecondPlayer(ref PadelPlayer newSecondPlayer)
        {
            second = ref newSecondPlayer;
        }

        public string GetNameOfSecondPlayer() =>
            $"{second.FirstName} {second.LastName}";

        // FEATURE: scoped modifier
        // Scoped is required here as a promise that
        // we won't attempt to store the reference outside the scope of this method,
        // this allows for a stack-allocated Span<> to be passed in safely
        public void SetWinCountToFirstValue(scoped Span<int> newScore)
        {
            if (!HasWinCount())
            {
                // If we don't assign a reference to the field then we'll end up
                // with a null reference exception once we try to assign a value
                wins = ref new WinsHolder().wins;
            }

            wins = newScore[0];
        }

        public bool HasWinCount() => Unsafe.IsNullRef(ref wins) == false;
    }

    #region Not interesting

    public readonly record struct PadelPlayer(
        string FirstName,
        string LastName,
        int BirthYear);

    private static PadelPlayer Galan = new()
    {
        FirstName = "Ale",
        LastName = "Galán",
        BirthYear = 1996
    };

    private static PadelPlayer Lebron = new()
    {
        FirstName = "Juan",
        LastName = "Lebrón",
        BirthYear = 1995
    };

    private class WinsHolder
    {
        public int wins;
    }

    #endregion
}