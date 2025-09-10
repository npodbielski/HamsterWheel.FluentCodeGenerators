using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

public static class AdditionalTextPredicates
{
    public static Func<AdditionalText, bool> InDirectory(string directory) =>
        af => Path.GetFileName(Path.GetDirectoryName(af.Path)) == directory;

    public static Func<AdditionalText, bool> InSecondLevelDirectory(string directory) =>
        af => Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(af.Path))) == directory;

    public static Func<AdditionalText, bool> FileNameIs(string fileName) =>
        af => Path.GetFileName(af.Path) == fileName;

    public static Func<AdditionalText, bool> FileNameExtensionIs(string extension) =>
        af => Path.GetExtension(af.Path) == extension;

    public static Func<AdditionalText, bool> FileNameEndsWith(string suffix) =>
        af => Path.GetFileName(af.Path).EndsWith(suffix);
}