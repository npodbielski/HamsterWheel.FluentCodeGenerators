namespace HamsterWheel.FluentCodeGenerators.Providers.Data;

public record AdditionalTextFileData(string FileName, string FilePath, string Content)
{
    public string? DirName => Path.GetFileName(Path.GetDirectoryName(FilePath)); 
    public string? DirPath => Path.GetDirectoryName(FilePath);
}