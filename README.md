# Introduction

Fluent Code Generators provides fluent API for [Roslyn Source Generators](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/#source-generators) that enable developers to write custom source code generators in more predictable and controllable way. Fluent Code Generators packages are compatible with any version of .NET that is supported by [Roslyn Incremental Generators](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md). Packages are distributed as .NET Standard.
This project is part of Hamster Wheel platform, dynamically configurable, extensible API that aim to be easy to use, secure solution for data manipulation of your choice. It is intended to be used for personal projects, hobbyist and small companies. 


## Reference links

- [Hamster Wheel](https://podbielski.it/why-hamster-wheel)
- [Roslyn Incremental Generators](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md)
- [Roslyn Source Generators](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/#source-generators)
- [Roslyn Incremental Generators Cookbook](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.cookbook.md)

## What's contained in this project

This project contains of two main parts:
- main package: HamsterWheel.FluentCodeGenerators that is Incremental Code Generators Fluent API
- and HamsterWheel.FluentCodeGenerators.Abstractions package that can be used to further develop extensions for APIs and functionalities missing from main package

# How to use

Below are all the information that you need to get you started using Flunt Code Generators.

## Getting started

First you must prepare new project that can be used as Roslyn Analyzer/Source Generator. This project must use .NET Standard 2.0 Framework moniker.

```xml
<TargetFramework>netstandard2.0</TargetFramework>
```

Then you must install package in this project.

```xml
<PackageReference Include="HamsterWheel.FluentCodeGenerators" Version="0.4.0" PrivateAssets="all" />
```

