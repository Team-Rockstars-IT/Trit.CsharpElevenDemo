namespace Trit.DemoConsole._3_File_local_types
{
    public static class Demo
    {
        public static Task Main()
        {
            WriteLine(_2_Pattern_matching.Demo.Main());

            return Task.CompletedTask;
        }
    }
}

// FEATURE: File-local types
namespace Trit.DemoConsole._2_Pattern_matching
{
    // This might end up something like:
    // <_>FD2E2ADF7177B7A8AFDDBC12D1634CF23EA1A71020F6A1308070A16400FB68FDE__Demo
    file class Demo
    {
        public static Task Main()
        {
            WriteLine("Only visible within this file");

            return Task.CompletedTask;
        }
    }
}