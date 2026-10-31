using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class SourceProductionContextExtensions
{
    extension(SourceProductionContext context)
    {
        public void ReportInformation(string message) =>
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor("information", "information", message, "info", DiagnosticSeverity.Info,
                    true), Location.None));

        public void ReportWarning(string message) =>
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor("warning", "warning", message, "warning", DiagnosticSeverity.Warning,
                    true), Location.None));

        public void ReportException(Exception e) =>
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor("exception", "failure", e.Message, "error", DiagnosticSeverity.Error,
                    true), Location.None));
    }
}