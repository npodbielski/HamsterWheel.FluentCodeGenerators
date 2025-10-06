using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Exceptions;

public class GeneratorException(string message) : Exception(message);

public class InvalidNumberOfGenericArgumentsException(int expectedNumberOfArguments, IName genericType, IName[] arguments)
    : GeneratorException(
        $"Invalid number of generic arguments provided: [{
            string.Join(", ", arguments.Select(n => n.ToString()))
        }]. Target type: '{genericType}' expects {expectedNumberOfArguments}");

public class IncorrectTypeUsageContextException<TExpected, TActual>()
    : GeneratorException($"Expected context to be of `{typeof(TExpected).FullName}` but got `{typeof(TActual).FullName}``");

public class DuplicatedParameterException(string name, string method)
    : GeneratorException($"Method with name : '{method}' already have parameter with name: '{name}'");

public class NotInitializedChunkException() : GeneratorException("Cannot read property of not initialized chunk");