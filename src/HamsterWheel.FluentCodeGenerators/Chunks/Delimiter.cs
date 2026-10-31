namespace HamsterWheel.FluentCodeGenerators.Chunks;

public record Delimiter
{
    private Delimiter(string? delimiterString, int? numberOfNewLines)
    {
        DelimiterString = delimiterString;
        NumberOfNewLines = numberOfNewLines;
        if (delimiterString is null && numberOfNewLines is null)
        {
            throw new ArgumentException(
                $"Either '{nameof(delimiterString)}' or '{nameof(numberOfNewLines)}' parameter needs to be set!");
        }
    }

    public bool IsNewLine() => NumberOfNewLines is not null;

    public static implicit operator Delimiter(string delimiterString) => new(delimiterString, numberOfNewLines: null);

    public static Delimiter SingleNewLine => new(null, 1);
    public static Delimiter DoubleNewLine => new(null, 2);
    public static Delimiter Empty => new("");

    public string? DelimiterString { get; }
    public int? NumberOfNewLines { get; }
}