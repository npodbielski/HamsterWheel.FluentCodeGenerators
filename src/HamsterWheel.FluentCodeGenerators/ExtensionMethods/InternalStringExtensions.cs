namespace HamsterWheel.FluentCodeGenerators;

internal static class InternalStringExtensions
{
    public static string Quote(this string str) => $"\"{str}\"";
    public static string TripleQuote(this string str) => $"\"\"\"\n{str}\n\"\"\"";
}