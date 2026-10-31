using Microsoft.CodeAnalysis;
using static System.IO.Path;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

public static class AdditionalTextPredicates
{
    public static Func<AdditionalText, bool> InDirectory(string directory) =>
        af => GetFileName(GetDirectoryName(af.Path)) == directory;

    public static Func<AdditionalText, bool> InSecondLevelDirectory(string directory) =>
        af => GetFileName(GetDirectoryName(GetDirectoryName(af.Path))) == directory;

    public static Func<AdditionalText, bool> FileNameIs(string fileName) =>
        af => GetFileName(af.Path) == fileName;

    public static Func<AdditionalText, bool> FileNameExtensionIs(string extension) =>
        af => GetExtension(af.Path) == extension;

    public static Func<AdditionalText, bool> FileNameEndsWith(string suffix) =>
        af => GetFileName(af.Path).EndsWith(suffix);
}