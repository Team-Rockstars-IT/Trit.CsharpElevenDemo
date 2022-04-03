namespace Trit.DemoConsole._4_Raw_string_literals;

public static class Demo
{
    public static Task Main()
    {
        WriteLine("""
        "
        1 cup white sugar
        ½ cup unsalted butter
        2 large eggs
        2 teaspoons vanilla extract
        1½ cups all-purpose flour
        1¾ teaspoons baking powder
        ½ cup milk
        "
        """);
        return Task.CompletedTask;
    }
}