using Microsoft.CodeAnalysis;

namespace Entish.SourceGenerator.Extensions;

public static class SymbolExtensions
{
    public static IEnumerable<IFieldSymbol> PickOptimalFields(this IEnumerable<IFieldSymbol> fields, ISymbol fieldOffsetAttributeType)
    {
        return fields
            .OrderBy(field => field.Type.CanSwapWithoutCast())
            .GroupBy(field => (int)field.GetAttributes()
                .FirstOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, fieldOffsetAttributeType))!
                .ConstructorArguments[0].Value!
            )
            .Select(fieldGroup => fieldGroup.FirstOrDefault());
    }

    /// <summary>
    /// Returns true if the type can be swapped without casting.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static bool CanSwapWithoutCast(this ITypeSymbol type)
        => type.SpecialType is
            SpecialType.System_UInt16 or SpecialType.System_UInt32 or
            SpecialType.System_UInt64 or SpecialType.System_UIntPtr;
}