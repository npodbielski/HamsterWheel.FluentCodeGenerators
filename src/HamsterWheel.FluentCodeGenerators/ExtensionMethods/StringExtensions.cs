namespace HamsterWheel.FluentCodeGenerators;

public static class StringExtensions
{
    extension(string str)
    {
        public string Quote() => $"\"{str}\"";
        public string TripleQuote() => $"\"\"\"\n{str}\n\"\"\"";
    }
}