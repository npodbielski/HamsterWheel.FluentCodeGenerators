using System.Globalization;
using System.Reflection;
using HamsterWheel.FluentCodeGenerators.Generators;

namespace HamsterWheel.FluentCodeGenerators.FluentApi;

public static class FluentApiSettings
{
    public static Assembly GeneratorAssembly { get; set; } = typeof(SourceCodeFileGeneratorBase).Assembly;
    public static CultureInfo DefaultCulture { get; set; } = CultureInfo.InvariantCulture;
}