using FluentAssertions;
using HamsterWheel.FluentCodeGenerators.FluentApi.Contexts;
using HamsterWheel.FluentCodeGenerators.Generators;
using HamsterWheel.FluentCodeGenerators.Tokens;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace HamsterWheel.FluentCodeGenerators.UnitTests.Generators;

public class OrchestratedIncrementalSourceCodeGeneratorUnitTests
{
    [Fact]
    public void Initialize_WhenNoGlobalUsings_ThenDoesNotRegisterPostInitializationOutput()
    {
        //arrange
        var testGenerator = new TestOrchestratedGenerator(withGlobalUsings: false);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly");

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        //assert
        var generatedTrees = newCompilation.SyntaxTrees.Select(t => t.FilePath).ToArray();
        generatedTrees.Should().NotContain(t => t.Contains("GlobalUsings"));
    }

    [Fact]
    public void Initialize_WhenGlobalUsingsProvided_ThenRegistersPostInitializationOutput()
    {
        //arrange
        var testGenerator = new TestOrchestratedGenerator(withGlobalUsings: true);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly");

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        //assert
        var globalUsingsTree = newCompilation.SyntaxTrees.FirstOrDefault(t => t.FilePath.Contains("GlobalUsings"));
        globalUsingsTree.Should().NotBeNull();
        globalUsingsTree.ToString().Should().Contain("global using System.Linq;");
    }

    [Fact]
    public void Initialize_WhenRegisterFileGenerator_ThenProducesOutput()
    {
        //arrange
        var testGenerator = new TestOrchestratedGenerator(withGlobalUsings: false);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly");

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        //assert
        var generatedTrees = newCompilation.SyntaxTrees.Select(t => t.FilePath).ToArray();
        generatedTrees.Should().Contain(t => t.Contains("TestGenerator"));
    }

    [Fact]
    public void Initialize_WhenActionThrows_ThenProducesErrorFile()
    {
        //arrange
        var testGenerator = new TestOrchestratedGenerator(withGlobalUsings: false, throwException: true);
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly");

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        //assert
        var errorTree = newCompilation.SyntaxTrees.FirstOrDefault(t => t.FilePath.Contains("_Error_From_"));
        errorTree.Should().NotBeNull();
        errorTree.ToString().Should().Contain("---TestGenerator---");
        errorTree.ToString().Should().Contain("InvalidOperationException");
    }

    [Fact]
    public void Initialize_WhenMultipleGeneratorsRegistered_ThenAllAreExecuted()
    {
        //arrange
        var testGenerator = new TestOrchestratedGenerator(withGlobalUsings: false);
        testGenerator.AddAdditionalGenerator("SecondGenerator", (pc, _) =>
            pc.AddSource("Second.g.cs", "// second"));
        var driver = CSharpGeneratorDriver.Create(testGenerator);
        var compilation = CSharpCompilation.Create("TestAssembly");

        //act
        driver.RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out _);

        //assert
        var generatedTrees = newCompilation.SyntaxTrees.Select(t => t.FilePath).ToArray();
        generatedTrees.Should().Contain(t => t.Contains("TestGenerator"));
        generatedTrees.Should().Contain(t => t.Contains("Second.g.cs"));
    }

    private class TestOrchestratedGenerator : OrchestratedIncrementalSourceCodeGenerator<string>
    {
        private readonly bool _withGlobalUsings;
        private readonly bool _throwException;

        public TestOrchestratedGenerator(bool withGlobalUsings, bool throwException = false)
        {
            _withGlobalUsings = withGlobalUsings;
            _throwException = throwException;

            RegisterFileGenerator("TestGenerator", (pc, _) =>
            {
                if (_throwException)
                    throw new InvalidOperationException("Test exception");

                pc.AddSource("TestGenerator.g.cs", "// test output");
            });
        }

        protected override IncrementalValueProvider<string> BuildRequest(IncrementalGeneratorInitializationContext context)
            => context.CompilationProvider.Select((c, _) => c.AssemblyName ?? "default");

        protected override IEnumerable<string> SetGlobalUsings()
            => _withGlobalUsings ? new[] { "System.Linq" } : [];

        public TestOrchestratedGenerator AddAdditionalGenerator(string name, Action<SourceProductionContext, string> action)
        {
            RegisterFileGenerator(name, action);
            return this;
        }
    }

    private class FileBasedOrchestratedGenerator : OrchestratedIncrementalSourceCodeGenerator<string>
    {
        public FileBasedOrchestratedGenerator()
        {
            RegisterFileGenerator<TestFileGenerator>(
                (pc, _) => new TestFileGenerator(pc, "gen", "GenNamespace"));

            RegisterFileGenerators("MultiGenerator", (pc, _) =>
                [new SimpleSourceCodeFileGenerator(pc, "multi1.g.cs"), new SimpleSourceCodeFileGenerator(pc, "multi2.g.cs")]);
        }

        protected override IncrementalValueProvider<string> BuildRequest(IncrementalGeneratorInitializationContext context)
            => context.CompilationProvider.Select((c, _) => c.AssemblyName ?? "default");
    }

    private class TestFileGenerator(SourceProductionContext pc, string directory, string ns)
        : SourceCodeFileGeneratorBase(pc, directory, ns)
    {
        protected override void Configure(IFileScopedNamespaceContext context)
        {
            context.WithClass(c => c.Named("GeneratedClass"));
        }
    }

    private class SimpleSourceCodeFileGenerator(SourceProductionContext pc, string fileName) : ISourceCodeFileGenerator
    {
        public void GenerateAndAdd() => pc.AddSource(fileName, "// generated");
    }
}
