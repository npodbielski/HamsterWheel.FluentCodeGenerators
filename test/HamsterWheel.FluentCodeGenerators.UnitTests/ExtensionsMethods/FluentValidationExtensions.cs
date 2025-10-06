using HamsterWheel.FluentCodeGenerators.Chunks;
using HamsterWheel.FluentCodeGenerators.UnitTests.Assertions;

namespace HamsterWheel.FluentCodeGenerators.UnitTests;

public static class FluentValidationExtensions
{
    public static ICodeChunkAssertions<T> Should<T>(this T instance) where T : ICodeChunk => new(instance);
}