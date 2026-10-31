namespace HamsterWheel.FluentCodeGenerators.Tokens;

public static class INameExtensions
{
    extension(IName name)
    {
        public string FirstLetterLower() => name.ToString()[0].ToString().ToLower();
        public PascalCaseName ToPascalCase() => name.ToString();
        public CamelCaseName ToCamelCase() => name.ToString();

        private bool IsInterface() =>
            name.ToString().StartsWith("I") && char.IsUpper(name.ToString()[1]);

        public CamelCaseName ToParameterName() =>
            name.IsInterface() ? new CamelCaseName(name.ToString()[1..]) : name.ToCamelCase();

        public CamelCaseName ToFieldName()
        {
            var nameAsString = name.ToString().TrimStart('_');
            return new CamelCaseName("_" + (name.IsInterface() ? new CamelCaseName(nameAsString[1..]) : nameAsString.ToCamelCase()));
        }

        public PascalCaseName ToPlural() => new(name.ToString().ToPlural());
        public PascalCaseName ToSingular() => new(name.ToString().ToSingular());

        public NameInNamespace InNamespace(INamespace targetNamespace) =>
            new(name.ToPascalCase(), new Namespace(targetNamespace.NamespaceAsString));
    }
}