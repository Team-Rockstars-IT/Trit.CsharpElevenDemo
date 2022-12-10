namespace Trit.DemoConsole._6_Ref_scoped_and_structs;

public static class Demo
{
    private readonly ref struct FourSongCD
    {
        // FEATURE: ref fields
        private readonly ref readonly SongData songOne;
        private readonly ref readonly SongData songTwo;
        private readonly ref readonly SongData songThree;
        private readonly ref readonly SongData songFour;

        public FourSongCD(ref SongData one,
            ref SongData two,
            ref SongData three,
            ref SongData four)
        {
            songOne = ref one;
            songTwo = ref two;
            songThree = ref three;
            songFour = ref four;
        }

        public int TotalByteCount =>
            songOne.DataLength
            + songTwo.DataLength
            + songThree.DataLength
            + songFour.DataLength;
    }

    private struct SongData
    {
        private const int MAX_SONG_LENGTH_IN_BYTES = 10_000;
        private unsafe fixed byte data[MAX_SONG_LENGTH_IN_BYTES];

        // FEATURE: Auto-default structs
        private bool lowQuality;

        // FEATURE: scoped modifier
        // Scoped is required here as a promise that
        // we won't attempt to store the reference outside the scope of this method,
        // this allows for a stack-allocated Span<> to be passed in safely
        public unsafe SongData(scoped Span<byte> input)
        {
            fixed (byte* fixedBuffer = data)
            fixed (byte* fixedInput = input)
            {
                Buffer.MemoryCopy(
                    fixedInput, fixedBuffer,
                    MAX_SONG_LENGTH_IN_BYTES, input.Length);
            }
        }

        public int DataLength => MAX_SONG_LENGTH_IN_BYTES;
    }

    public static Task Main()
    {
        RandomNumberGenerator.GetBytes(1000);
        long before = GC.GetTotalAllocatedBytes(precise: true);

        Span<byte> soundInputBuffer = stackalloc byte[1000];
        var one = new SongData(RecordSong(soundInputBuffer));
        var two = new SongData(RecordSong(soundInputBuffer));
        var three = new SongData(RecordSong(soundInputBuffer));
        var four = new SongData(RecordSong(soundInputBuffer));

        var fourBuffers = new FourSongCD(ref one, ref two, ref three, ref four);

        long after = GC.GetTotalAllocatedBytes(precise: true);

        WriteLine($"Allocated {after - before} bytes of managed memory " +
                  $"(stack-allocated {fourBuffers.TotalByteCount + 1000} bytes)");

        before = GC.GetTotalAllocatedBytes(precise: true);
        byte[] managedArray = RandomNumberGenerator.GetBytes(1000);
        after = GC.GetTotalAllocatedBytes(precise: true);

        WriteLine($"Allocated {after - before} byte of managed memory " +
                  $"for an array with {managedArray.Length} bytes");

        return Task.CompletedTask;
    }

    private static Span<byte> RecordSong(Span<byte> data)
    {
        RandomNumberGenerator.Fill(data);
        return data;
    }
}