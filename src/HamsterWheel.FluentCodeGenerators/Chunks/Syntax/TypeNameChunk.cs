using System.Text;
using HamsterWheel.FluentCodeGenerators.Exceptions;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Syntax;

public class TypeNameChunk(string typeName, bool forAttribute = false) : INamedChunk
{
    private readonly GenericTypeArgumentsChunk _genericArgsChunk = new();

    private bool _isArray;
    private string _typeName = typeName;
    internal int? ExpectedNumberOfTypeArguments { get; set; }
    public PascalCaseName Name => new(MapTypeToKeyword(OmitGenericArgumentsNumber(_typeName)));
    IName INamedChunk.Name => Name;
    public bool IsNullable { get; private set; }

    public void ReplaceName(string name) => _typeName = name;

    public bool AppendChunks(StringBuilder stringBuilder)
    {
        if (ExpectedNumberOfTypeArguments is not null && ExpectedNumberOfTypeArguments != _genericArgsChunk.Types.Count)
        {
            ThrowInvalidTypeArguments();
        }

        if (Name.ToString().EndsWith(nameof(Attribute)) && forAttribute)
        {
            stringBuilder.Append(Name.ToString().Replace(nameof(Attribute), ""));
        }
        else
        {
            stringBuilder.Append(Name);
        }

        _genericArgsChunk.AppendChunks(stringBuilder);
        if (_isArray)
        {
            stringBuilder.Append("[]");
        }

        if (!IsNullable)
        {
            return true;
        }

        if (Name.ToString().StartsWith(From<Task>().Name))
        {
            stringBuilder.Replace(">", "?>");
        }
        else
        {
            stringBuilder.Append('?');
        }

        return true;
    }

    public void AddGenericArgument(TypeNameChunk argChunk)
    {
        if (ExpectedNumberOfTypeArguments is not null && ExpectedNumberOfTypeArguments == _genericArgsChunk.Types.Count)
        {
            ThrowInvalidTypeArguments();
        }

        _genericArgsChunk.AddArg(argChunk);
    }

    public void MakeNullable() => IsNullable = true;

    public void MakeArray() => _isArray = true;

    public static TypeNameChunk From<T>()
    {
        var type = typeof(T);
        return From(type);
    }

    public static TypeNameChunk From(Type type, bool forAttribute = false)
    {
        var chunk = new TypeNameChunk(type.Name, forAttribute);
        if (type.GenericTypeArguments is not null)
        {
            foreach (var typeArgument in type.GenericTypeArguments)
            {
                chunk.AddGenericArgument(new TypeNameChunk(typeArgument.Name));
            }
        }

        return chunk;
    }

    public static TypeNameChunk Void { get; } = From(typeof(void));
    public static TypeNameChunk Object => From(typeof(object));
    public static TypeNameChunk String => From(typeof(string));

    public static string MapTypeToKeyword(string typeName)
    {
        //bool
        if (typeName == nameof(Boolean))
        {
            typeName = "bool";
        }

        //numeric
        if (typeName == nameof(SByte))
        {
            typeName = "sbyte";
        }
        else if (typeName == nameof(Byte))
        {
            typeName = "byte";
        }
        else if (typeName == nameof(Int16))
        {
            typeName = "short";
        }
        else if (typeName == nameof(UInt16))
        {
            typeName = "ushort";
        }
        else if (typeName == nameof(Int32))
        {
            typeName = "int";
        }
        else if (typeName == nameof(UInt32))
        {
            typeName = "uint";
        }
        else if (typeName == nameof(Int64))
        {
            typeName = "long";
        }
        else if (typeName == nameof(IntPtr))
        {
            typeName = "nint";
        }
        else if (typeName == nameof(UIntPtr))
        {
            typeName = "nuint";
        }
        else if (typeName == nameof(Decimal))
        {
            typeName = "decimal";
        }
        //string
        else if (typeName == nameof(System.String) || typeName == typeof(string[]).Name)
        {
            typeName = typeName.Replace("String", "string");
        }
        //object
        else if (typeName == nameof(Object))
        {
            typeName = "object";
        }
        //void
        else if (typeName == "Void")
        {
            typeName = "void";
        }

        return typeName;
    }

    private void ThrowInvalidTypeArguments() =>
        throw new InvalidNumberOfGenericArgumentsException(ExpectedNumberOfTypeArguments ?? 0, (PascalCaseName)_typeName,
            _genericArgsChunk.Types.Select(t => t.Name).Cast<IName>().ToArray());

    private static string OmitGenericArgumentsNumber(string typeName) => typeName.Split('`')[0];
}