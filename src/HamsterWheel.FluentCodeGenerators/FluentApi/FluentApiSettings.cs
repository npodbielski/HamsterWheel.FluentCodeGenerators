using System.Globalization;
using System.Reflection;
using HamsterWheel.FluentCodeGenerators.Generators;

namespace HamsterWheel.FluentCodeGenerators.FluentApi;

public static class FluentApiSettings
{
    public static Assembly GeneratorAssembly { get; set; } = typeof(SourceCodeFileGeneratorBase).Assembly;
    public static bool AddGeneratedCodeAttribute { get; set; } = true;
    public static CultureInfo DefaultCulture { get; set; } = CultureInfo.InvariantCulture;

    internal static FluentApiSettingsSnapshot Snapshot() => new();
}