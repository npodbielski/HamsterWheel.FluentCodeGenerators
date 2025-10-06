using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class AdditionalTextExtensions
{
    public static string GetTextAsString(this AdditionalText additionalText) =>
        additionalText.GetText()?.ToString() ?? "";
}