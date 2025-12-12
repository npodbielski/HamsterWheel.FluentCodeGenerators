using Microsoft.CodeAnalysis;

namespace HamsterWheel.FluentCodeGenerators;

public static class INamedSymbolExtensions
{
    extension(INamedTypeSymbol nameTypeSymbol)
    {
        public ISymbol? GetMemberFromInheritanceTree(string memberName) =>
            GetMembersFromInheritanceTree(nameTypeSymbol, m => m.Name == memberName).FirstOrDefault();

        public List<ISymbol> GetMembersFromInheritanceTree(Func<ISymbol, bool> memberPredicate)
        {
            List<ISymbol> matches = [];
            var currentType = nameTypeSymbol;
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
    }
}