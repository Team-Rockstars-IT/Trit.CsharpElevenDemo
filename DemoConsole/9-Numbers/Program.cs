using System.Numerics;

namespace Trit.DemoConsole._9_Numbers;

public static class Demo
{
    public static Task Main()
    {
        // FEATURE: numeric UIntPtr
        nuint one = 1;
        WriteLine($"1 + 1 = {Add(one, one)}");

        WriteLine($"Red + Blue = {Add(Color.Red, Color.Blue)}");

        // FEATURE: Unsigned right shift operator
        WriteLine($"1 >>> -1 = {-1 >>> 1}");

        WriteLine($"A + 5 = {checked(new HexDigit('A') + 5)}");
        WriteLine($"A >> 2 = {new HexDigit('A') >> 2}");
        try
        {
            WriteLine($"A + 9 = {checked(new HexDigit('A') + 9)}");
        }
        catch (OverflowException)
        {
            WriteLine("Overflow!");
        }

        return Task.CompletedTask;
    }

    private static T Add<T>(params T[] adds)
        where T :
        IAdditionOperators<T, T, T>,
        IAdditiveIdentity<T, T>
    {
        // FEATURE: Static abstract members in interfaces
        T accumulator = T.AdditiveIdentity;
        foreach (T add in adds)
        {
            accumulator += add;
        }

        return accumulator;
    }

    public record struct HexDigit(char Digit)
        : IShiftOperators<HexDigit, HexDigit, HexDigit>,
            IAdditionOperators<HexDigit, HexDigit, HexDigit>
    {
        private int Integer => Digit >= 'A' ? Digit - '7' : Digit - '0';

        // FEATURE: Relaxing shift operator requirements
        public static HexDigit operator <<(HexDigit value, HexDigit shiftAmount)
        {
            return value.Integer << shiftAmount.Integer;
        }

        public static HexDigit operator >> (HexDigit value, HexDigit shiftAmount)
        {
            return value.Integer >> shiftAmount.Integer;
        }

        public static HexDigit operator >>> (HexDigit value, HexDigit shiftAmount)
        {
            return value.Integer >>> shiftAmount.Integer;
        }

        // FEATURE: Checked user-defined operators
        public static HexDigit operator checked +(HexDigit left, HexDigit right)
        {
            int result = left.Integer + right.Integer;

            if (result > 16) throw new OverflowException();

            return result;
        }

        public static HexDigit operator +(HexDigit left, HexDigit right)
        {
            return (left.Integer + right.Integer) % 16;
        }

        public static implicit operator HexDigit(int value) =>
            new((char)(value > 9 ? value + '7' : value + '0'));

        public override string ToString() => new(Digit, 1);
    }

    #region Not interesting

    public record Color :
        IAdditionOperators<Color, Color, Color>,
        IAdditiveIdentity<Color, Color>
    {
        private readonly ColorName colorName;

        private Color(ColorName colorName)
        {
            this.colorName = colorName;
        }

        public static Color Red { get; } = new(ColorName.Red);
        public static Color Green { get; } = new(ColorName.Green);
        public static Color Blue { get; } = new(ColorName.Blue);
        public static Color Transparent { get; } = new(ColorName.Transparent);

        public static Color AdditiveIdentity => Transparent;

        public static Color operator +(Color left, Color right)
        {
            return (left.colorName, right.colorName) switch
            {
                _ when left.colorName == right.colorName => left,
                (ColorName.Transparent, _) => right,
                (_, ColorName.Transparent) => left,
                (ColorName.Red, ColorName.Blue) => new Color(ColorName.Purple),
                (ColorName.Blue, ColorName.Red) => new Color(ColorName.Purple),
                _ => throw new NotSupportedException()
            };
        }

        public override string ToString() => colorName.ToString();

        private enum ColorName
        {
            Red,
            Green,
            Blue,
            Purple,
            Transparent
        }
    }

    #endregion
}