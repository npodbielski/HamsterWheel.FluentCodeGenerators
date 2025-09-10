using System.Text;

namespace HamsterWheel.FluentCodeGenerators.Chunks.Body;

public abstract class BodyChunk : ICodeChunk
{
    public abstract bool AppendChunks(StringBuilder stringBuilder);

    public override string ToString() => this.Build();
}