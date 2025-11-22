namespace HamsterWheel.FluentCodeGenerators;

public static class StringExtensions
{
    public static string Quote(this string str) => $"\"{str}\"";
    public static string TripleQuote(this string str) => $"\"\"\"\n{str}\n\"\"\"";
}