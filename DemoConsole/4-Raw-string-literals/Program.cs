namespace Trit.DemoConsole._4_Raw_string_literals;

public static class Demo
{
    public static Task Main()
    {
        const string artistNameProperty = "Name";
        // FEATURE: Raw string literals
        const string code = $"""
        import urllib.request, json

        url = "https://raw.githubusercontent.com/Team-Rockstars-IT/MusicLibrary/main/artists.json"
        contents = urllib.request.urlopen(url).read()
        artists = json.loads(contents)

        print(artists[0]["{artistNameProperty}"])
        """;

        File.WriteAllText("temp.py", code);
        using Process pythonProc = Process.Start(new ProcessStartInfo()
        {
            FileName = Environment.OSVersion.Platform == PlatformID.Unix ? "python3" : "python",
            Arguments = "temp.py",
            RedirectStandardOutput = true
        }) ?? throw new InvalidOperationException("Failed to start process");
        pythonProc.WaitForExit();
        File.Delete("temp.py");

        WriteLine(pythonProc.StandardOutput.ReadLine());

        return Task.CompletedTask;
    }
}