# Agent instructions

Standard rules for AI coding agents (and humans) working in this repository.

## Code style

- **XML doc summaries are multi-line**: open and close tags on their own lines, never text
  flowing after `<summary>` or before `</summary>`.

```csharp
/// <summary>
/// Equivalent CLR type if exists.
/// </summary>
```

- **No unused usings.** Before pushing, run `jb inspectcode FluentCodeGenerators.slnx --no-build`
  and fix every `RedundantUsingDirective` in the files the change touches.

## Members order

In a type body: properties and instance members first (below the constructor), then the
`public static` factory methods, then `private static` helpers.
