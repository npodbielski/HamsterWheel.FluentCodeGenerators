using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class SourceProductionContextExtensions
{
    public static void ReportInformation(this SourceProductionContext context, string message) =>
        context.ReportDiagnostic(Diagnostic.Create(
            new DiagnosticDescriptor("information", "information", message, "info", DiagnosticSeverity.Info,
                true), Location.None));

    public static void ReportWarning(this SourceProductionContext context, string message) =>
        context.ReportDiagnostic(Diagnostic.Create(
            new DiagnosticDescriptor("warning", "warning", message, "warning", DiagnosticSeverity.Warning,
                true), Location.None));

    public static void ReportException(this SourceProductionContext context, Exception e) =>
        context.ReportDiagnostic(Diagnostic.Create(
            new DiagnosticDescriptor("exception", "failure", e.Message, "error", DiagnosticSeverity.Error,
                true), Location.None));
}