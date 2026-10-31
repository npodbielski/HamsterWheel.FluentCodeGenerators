# Deferred `.When()` API — Final Implementation Plan

## Goal

Create `*Definition` interfaces that expose internal FluentCodeGenerators data from member contexts. Add a `.When(Func<I*Definition, bool>)` extension method so users can conditionally discard members after configuration. Chunks get an `_emit` flag checked in `AppendChunks`.

## Design Decisions

- Type info uses concrete `NameInNamespace` record (not `INameInNamespaceToken` interface)
- Declaring type exposed as single `NameInNamespace DeclaringType` (combines name + namespace)
- No `PendingMemberEntry` struct -- chunks get `_emit: bool` flag, checked in `AppendChunks`
- `.When()` extension stores predicate on context; after configure returns, ClassContext builds definition and evaluates it, flipping `_emit` if false

## Implementation Steps

### Step 1: Create definition interfaces in Abstractions

**`src/HamsterWheel.FluentCodeGenerators.Abstractions/Members/IParameterDefinition.cs`**
```csharp
public interface IParameterDefinition
{
    string Name { get; }
    NameInNamespace Type { get; }
}
```

**`src/HamsterWheel.FluentCodeGenerators.Abstractions/Members/IMemberDefinition.cs`** -- all definitions share declaring type + name:
```csharp
public interface IMemberDefinition
{
    NameInNamespace DeclaringType { get; }  // class name + namespace combined
    string Name { get; }
}

public interface IMethodDefinition : IMemberDefinition
{
    NameInNamespace? ReturnType { get; }
    EquatableArray<IParameterDefinition> Parameters { get; }
}

public interface IPropertyDefinition : IMemberDefinition
{
    NameInNamespace Type { get; }
}

public interface IFieldDefinition : IMemberDefinition
{
    NameInNamespace Type { get; }
    bool IsConst { get; }
}
```

**`src/HamsterWheel.FluentCodeGenerators.Abstractions/Members/MemberConditionExtensions.cs`** -- extension method stubs:
```csharp
public static class MemberConditionExtensions
{
    public static IMethodContext When(this IMethodContext context, Func<IMethodDefinition, bool> predicate);
    public static IPropertyContext When(this IPropertyContext context, Func<IPropertyDefinition, bool> predicate);
    public static IFieldContext When(this IFieldContext context, Func<IFieldDefinition, bool> predicate);
}
```

### Step 2: Add `_emit` flag to chunks + track namespace on types

**ParameterDefinitionChunk** -- add two fields:
```csharp
private NameInNamespace? _fullType;  // captured from WithParameter<T>
// No _emit on parameters since they are always emitted with their method
```

Add `SetFullType(NameInNamespace)` and `GetFullType()` methods.

**MethodDefinitionChunk, PropertyDefinitionChunk, FieldDefinitionChunk** -- add:
```csharp
private bool _emit = true;
public void SuppressEmit() => _emit = false;
```

Override `AppendChunks` to short-circuit when `_emit` is false.

Update all `WithParameter<T>` overloads in `MethodContext` extension class to capture namespace on the chunk. Same for `OfType<T>` in `PropertyContext` and `FieldContext`.

### Step 3: Contexts hold predicate slot + reference to their chunk

Each member context (`MethodContext`, `PropertyContext`, `FieldContext`) gets two private fields:
```csharp
private readonly Func<IMethodDefinition, bool>? _whenPredicate;
private readonly MethodDefinitionChunk _chunk;  // already exists, just exposed for suppress
```

Actually simpler: each context stores a predicate via the `.When()` call. After configure returns, ClassContext builds definition from chunk data and evaluates the predicate. If false, calls `SuppressEmit()` on the chunk.

### Step 4: Wire `.When()` in ClassContext after configure callback

`ClassContext.WithMethod` flow becomes:
```csharp
public IClassContext WithMethod(Action<IMethodContext> configure)
{
    var chunk = new MethodDefinitionChunk("MyMethod");
    classChunk.AddMethod(chunk);  // Add immediately
    
    Func<IMethodDefinition, bool>? predicate = null;
    var context = MethodContext.From(this, chunk, p => predicate = p);
    configure(context);
    
    if (predicate != null)
    {
        var definition = BuildMethodDefinition(chunk);
        if (!definition(definition))
            chunk.SuppressEmit();
    }
    
    return this;
}
```

Same pattern for `WithField` and `WithProp`.

### Step 5: Build definition objects from chunk data

Helper methods in ClassContext (or internal static class):
- `BuildMethodDefinition(MethodDefinitionChunk, NameInNamespace declaringType)` -> `IMethodDefinition`
- `BuildPropertyDefinition(PropertyDefinitionChunk, NameInNamespace declaringType)` -> `IPropertyDefinition`
- `BuildFieldDefinition(FieldDefinitionChunk, NameInNamespace declaringType)` -> `IFieldDefinition`

### Step 6: Write tests

Test `.When()` on methods, fields, properties with predicates that keep/discard members.

## Files Summary

**New (Abstractions):**
- `Members/IParameterDefinition.cs`
- `Members/IMemberDefinition.cs`
- `Members/MemberConditionExtensions.cs`

**Modified:**
- `Chunks/MethodDefinitionChunk.cs` -- add `_emit` flag + `SuppressEmit()`, short-circuit in `AppendChunks`
- `Chunks/PropertyDefinitionChunk.cs` -- same
- `Chunks/FieldDefinitionChunk.cs` -- same
- `Chunks/Syntax/ParameterDefinitionChunk.cs` -- add `_fullType: NameInNamespace?` field + getter/setter
- `FluentApi/Contexts/MethodContext.cs` -- accept predicate setter, capture namespace in `WithParameter<T>`
- `FluentApi/Contexts/PropertyContext.cs` -- accept predicate setter, capture namespace in `OfType<T>`
- `FluentApi/Contexts/FieldContext.cs` -- accept predicate setter, capture namespace in `OfType<T>`
- `FluentApi/Contexts/ClassContext.cs` -- build definition after configure, evaluate predicate, call `SuppressEmit()`
