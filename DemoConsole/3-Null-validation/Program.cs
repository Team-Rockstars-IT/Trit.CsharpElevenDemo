namespace Trit.DemoConsole._3_Null_validation;

public static class Demo
{
    public static Task Main()
    {
        try
        {
            var person = new Person("Albert", null, "Einstein");
            person.Bark("Hello there");
            person.BarkAt(otherPerson: null!);
            person.BarkAt(new Person(otherPerson: null!));
        }
        catch (ArgumentNullException ex)
        {
            WriteLine("Free null checking!");
            WriteLine(ex);
        }

        return Task.CompletedTask;
    }

    public record Person(
        string FirstName!!,
        string? MiddleName,
        string LastName!!)
    {
        public Person(Person otherPerson!!)
        {
            FirstName = otherPerson.FirstName;
            MiddleName = otherPerson.MiddleName;
            LastName = otherPerson.LastName;
        }

        public void Bark(string message!!)
        {
            WriteLine(message);
        }

        public void BarkAt(Person otherPerson!!)
        {
            WriteLine($"Hi {otherPerson.FirstName} " +
                      $"{otherPerson.MiddleName} " +
                      $"{otherPerson.LastName}");
        }
    }
}