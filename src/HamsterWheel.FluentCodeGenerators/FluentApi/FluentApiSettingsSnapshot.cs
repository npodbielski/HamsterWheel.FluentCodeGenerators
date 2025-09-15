using System.Globalization;
using System.Reflection;

namespace HamsterWheel.FluentCodeGenerators.FluentApi;

public class FluentApiSettingsSnapshot
{
    public Assembly GeneratorAssembly { get; set; } = FluentApiSettings.GeneratorAssembly;
    public bool AddGeneratedCodeAttribute { get; set; } = FluentApiSettings.AddGeneratedCodeAttribute;
    public CultureInfo DefaultCulture { get; set; } = FluentApiSettings.DefaultCulture;
}