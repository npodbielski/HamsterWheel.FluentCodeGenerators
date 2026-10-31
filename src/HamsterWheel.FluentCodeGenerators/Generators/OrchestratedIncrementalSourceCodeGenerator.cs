using System.Text;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators.Generators;

public abstract class OrchestratedIncrementalSourceCodeGenerator<T> : IOrchestratedIncrementalSourceCodeGenerator<T>
    where T : IEquatable<T>
{
    private readonly List<(Action<SourceProductionContext, T>, string)> _generationActions = [];

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        AddGlobalUsings(context);
        var requestProvider = BuildRequest(context);

        foreach (var (action, name) in _generationActions)
        {
            context.RegisterSourceOutput(requestProvider,
                (pc, request) => EmitErrorDetails(pc, name,
                    () => action(pc, request)));
        }
    }

    private void AddGlobalUsings(IncrementalGeneratorInitializationContext context)
    {
        var namespaces = SetGlobalUsings().ToArray();
        if (!namespaces.Any()) return;
        var stringBuilder = new StringBuilder();
        foreach (var ns in namespaces)
        {
            stringBuilder.AppendLine($"global using {ns};");
        }

        context.RegisterPostInitializationOutput(ctx =>
            ctx.AddSource("GlobalUsings.g.cs", stringBuilder.ToString()));
    }

    protected virtual IEnumerable<string> SetGlobalUsings() => [];

    protected abstract IncrementalValueProvider<T> BuildRequest(IncrementalGeneratorInitializationContext context);

    protected void RegisterFileGenerator<TGenerator>(Func<SourceProductionContext, T, TGenerator> builder)
        where TGenerator : ISourceCodeFileGenerator =>
        RegisterFileGenerator(typeof(TGenerator).Name,
            (context, request) => builder(context, request).GenerateAndAdd());

    protected void RegisterFileGenerator(string name, Action<SourceProductionContext, T> action) =>
        _generationActions.Add((action, name));

    protected void RegisterFileGenerators(string name, Func<SourceProductionContext, T, IEnumerable<ISourceCodeFileGenerator>> action) =>
        _generationActions.Add(((context, request) =>
        {
            foreach (var generator in action(context, request))
            {
                generator.GenerateAndAdd();
            }
        }, name));

    private static void EmitErrorDetails(SourceProductionContext pc, string generator, Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            var details = string.Join("\n", $"---{generator}---\n{ex}".Split('\n').Select(l => "// " + l));
            pc.AddSource($"_Error_From_{generator}.g.cs", details);
        }
    }
}