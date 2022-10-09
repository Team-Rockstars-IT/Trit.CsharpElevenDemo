namespace Trit.DemoConsole._4_Raw_string_literals;

public static class Demo
{
    public static Task Main()
    {
        // FEATURE: Raw string literals
        const string code = """
        import urllib.request, json

        url = "https://raw.githubusercontent.com/Team-Rockstars-IT/MusicLibrary/main/artists.json"
        contents = urllib.request.urlopen(url).read()
        artists = json.loads(contents)

        print(artists[0]["Name"])
        """;

        File.WriteAllText("temp.py", code);
        using var pythonProc = Process.Start(new ProcessStartInfo()
        {
            FileName = "python",
            Arguments = "temp.py",
            RedirectStandardOutput = true
        });
        pythonProc.WaitForExit();
        File.Delete("temp.py");

        WriteLine(pythonProc.StandardOutput.ReadLine());

        return Task.CompletedTask;
    }
}