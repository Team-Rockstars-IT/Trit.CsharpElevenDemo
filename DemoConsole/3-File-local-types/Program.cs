﻿namespace Trit.DemoConsole._3_File_local_types
{
    public static class Demo
    {
        public static Task Main()
        {
            return _2_Pattern_matching.Demo.Main();
        }
    }
}

namespace Trit.DemoConsole._2_Pattern_matching
{
    // This might end up something like:
    // `Trit.DemoConsole._2_Pattern_matching.<Program>F4__Demo`
    // FEATURE: File-local types
    file static class Demo
    {
        public static Task Main()
        {
            WriteLine("Only visible within this file");

            return Task.CompletedTask;
        }
    }
}