using System.Reflection;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils;

public static class ObjectExtensions
{
    public static string GetThisObjectTypeAssemblyVersion(this object obj) =>
        obj.GetType().Assembly.GetCustomAttributesData()
            .FirstOrDefault(t => t.AttributeType == typeof(AssemblyFileVersionAttribute))?.ConstructorArguments.First()
            .Value as string ?? throw new InvalidOperationException("Assembly file version attribute not found");
}