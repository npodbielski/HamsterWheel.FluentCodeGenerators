using HamsterWheel.FluentCodeGenerators.Providers.Data;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Providers.Transformations;

public static class AdditionalTextSelectors
{
    public static string FileContent(AdditionalText af, CancellationToken _) => af.GetTextAsString();

    public static T ContentToEnum<T>(string? enumValue, CancellationToken _) where T : struct, Enum
        => Enum.TryParse(enumValue ?? default(T).ToString(), true, out T result) ? result : default;

    public static AdditionalTextFileData FileNamePathAndContent(AdditionalText af, CancellationToken t) =>
        new(Path.GetFileName(af.Path), af.Path, FileContent(af, t));
}