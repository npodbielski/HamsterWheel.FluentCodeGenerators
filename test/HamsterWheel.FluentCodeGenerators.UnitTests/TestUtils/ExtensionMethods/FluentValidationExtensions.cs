using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.Assertions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.TestUtils.ExtensionMethods;

public static class FluentValidationExtensions
{
    public static ICodeChunkAssertions<T> Should<T>(this T instance) where T : ICodeChunk => new(instance);
}