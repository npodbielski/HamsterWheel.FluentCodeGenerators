using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class INamedTypeSymbolExtensions
{
    extension(INamedTypeSymbol symbol)
    {
        public ISymbol? GetMemberFromInheritanceTree(string memberName) => symbol
            .GetMembersFromInheritanceTree(m => m.Name == memberName).FirstOrDefault();

        public List<ISymbol> GetMembersFromInheritanceTree(Func<ISymbol, bool> memberPredicate)
        {
            List<ISymbol> matches = [];
            var currentType = symbol;
            while (currentType is not null)
            {
                var members = currentType.GetMembers();
                var matchedMembers = members.Where(memberPredicate).ToArray();
                if (matchedMembers.Length > 0)
                {
                    matches.AddRange(matchedMembers);
                }

                currentType = currentType.BaseType;
            }

            return matches;
        }

        public bool IsType(Type type) => symbol.ToDisplayString() == type.FullName;

        public bool IsType<TType>() => symbol.IsType(typeof(TType));
    }
}