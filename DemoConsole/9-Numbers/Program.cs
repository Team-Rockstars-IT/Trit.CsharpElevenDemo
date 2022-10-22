using System.Numerics;

namespace Trit.DemoConsole._9_Numbers;

public static class Demo
{
    public static Task Main()
    {
        WriteLine($"1 + 1 = {Add(1, 1)}");

        WriteLine($"Red + Blue = {Add(new Color(ColorName.Red), new Color(ColorName.Blue))}");

        return Task.CompletedTask;
    }

    private static T Add<T>(T left, T right) where T: IAdditionOperators<T, T, T>
    {
        return left + right;
    }

    #region Not interesting

    public record Color(ColorName ColorName) : IAdditionOperators<Color, Color, Color>
    {
        public static Color operator +(Color left, Color right)
        {
            return (left.ColorName, right.ColorName) switch
            {
                _ when left.ColorName == right.ColorName => left,
                (ColorName.Red, ColorName.Blue) => new Color(ColorName.Purple),
                (ColorName.Blue, ColorName.Red) => new Color(ColorName.Purple),
                _ => throw new NotSupportedException()
            };
        }

        public override string ToString() => ColorName.ToString();
    }

    public enum ColorName
    {
        Red,
        Green,
        Blue,
        Purple
    }

    #endregion
}