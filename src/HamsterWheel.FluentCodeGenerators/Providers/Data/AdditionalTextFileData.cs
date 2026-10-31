using static System.IO.Path;

namespace HamsterWheel.FluentCodeGenerators.Providers.Data;

public record AdditionalTextFileData(string FileName, string FilePath, string Content)
{
    public string? DirName => GetFileName(GetDirectoryName(FilePath));
    public string? DirPath => GetDirectoryName(FilePath);
}