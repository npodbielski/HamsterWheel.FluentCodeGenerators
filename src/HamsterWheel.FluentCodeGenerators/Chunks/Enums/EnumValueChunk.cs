using System.Text;
using HamsterWheel.FluentCodeGenerators.Chunks.Base;
using HamsterWheel.FluentCodeGenerators.Tokens;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Enums;

public class EnumValueChunk(string name, int? numericValue = null) : AttributesSetChunk
{
    private readonly PascalCaseName _name = new(name);

    public override bool AppendChunks(StringBuilder stringBuilder)
    {
        if (base.AppendChunks(stringBuilder))
        {
            stringBuilder.AppendLine();
        }

        stringBuilder.Append(_name);
        if (numericValue is not null)
        {
            stringBuilder.Append(" = " + numericValue);
        }

        stringBuilder.Append(",");

        return true;
    }
}