namespace HamsterWheel.FluentCodeGenerators.External;

public record ClrTypeInfo(Type Type, bool IsNullable = false)
{
    public static implicit operator ClrTypeInfo(Type type) => new(type);
}