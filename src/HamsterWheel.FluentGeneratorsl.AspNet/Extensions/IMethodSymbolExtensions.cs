using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentGenerators.AspNet;

public static class IMethodSymbolExtensions
{
    extension(IMethodSymbol symbol)
    {
        public AttributeData[] GetProducesResponseTypeAttributes() =>
            symbol.GetAttributes().Where(a => a.AttributeClass?.Name == nameof(ProducesResponseTypeAttribute))
                .ToArray();

        public INamedTypeSymbol? GetResponseFor200Ok()
        {
            var okResponseAttribute = symbol.GetProducesResponseTypeAttributes()
                .FirstOrDefault(a =>
                    a.ConstructorArguments.First().Value?.ToString() == ((int)HttpStatusCode.OK).ToString());
            return okResponseAttribute?.NamedArguments.FirstOrDefault().Value.Value as INamedTypeSymbol;
        }

        public INamedTypeSymbol? GetResponseFor201Created()
        {
            var okResponseAttribute = symbol.GetProducesResponseTypeAttributes()
                .FirstOrDefault(a =>
                    a.ConstructorArguments.First().Value?.ToString() == ((int)HttpStatusCode.Created).ToString());
            return okResponseAttribute?.NamedArguments.FirstOrDefault().Value.Value as INamedTypeSymbol;
        }

        public bool Have307Response() =>
            symbol.GetProducesResponseTypeAttributes()
                .FirstOrDefault(a =>
                    a.ConstructorArguments.First().Value?.ToString() ==
                    ((int)HttpStatusCode.TemporaryRedirect).ToString()) != null;

        public AttributeData? GetHttpGetAttr() =>
            symbol.GetAttributes().FirstOrDefault(a =>
                a.AttributeClass?.Name is nameof(HttpGetAttribute));
    }
}