namespace HamsterWheel.FluentCodeGenerators.External;

[Obsolete("Use TypeToken; it carries the loaded type in Runtime.")]
public record ClrTypeInfo(Type Type, bool IsNullable = false)
{
    public static implicit operator ClrTypeInfo(Type type) => new(type);
}