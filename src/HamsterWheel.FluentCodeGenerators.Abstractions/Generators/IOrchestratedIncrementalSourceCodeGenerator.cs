using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Generators;

public interface IOrchestratedIncrementalSourceCodeGenerator<T> : IIncrementalGenerator
    where T : IEquatable<T>;