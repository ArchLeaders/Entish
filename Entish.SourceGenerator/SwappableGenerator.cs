using System.Collections.Immutable;
using Entish.SourceGenerator.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Entish.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class SwappableGenerator : IIncrementalGenerator
{
    internal const string AttributeTypeName = "Entish.Attributes.SwappableAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<StructDeclarationSyntax> attributedClassProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(AttributeTypeName,
                predicate: (_, _) => true,
                transform: (n, _) => (StructDeclarationSyntax)n.TargetNode
            );

        IncrementalValueProvider<(Compilation Left, ImmutableArray<StructDeclarationSyntax> Right)> compilation
            = context.CompilationProvider.Combine(attributedClassProvider.Collect());

        context.RegisterSourceOutput(compilation,
            (spc, source) => { Execute(spc, source.Left, source.Right); }
        );
    }

    private void Execute(SourceProductionContext context, Compilation compilation, ImmutableArray<StructDeclarationSyntax> types)
    {
        SwappableBuilder builder = new(context, compilation);

        IEnumerable<INamedTypeSymbol> symbols = types
            .Select(x => (compilation.GetSemanticModel(x.SyntaxTree).GetDeclaredSymbol(x) as INamedTypeSymbol)!)
            .Where(x => x is not null);

        foreach (INamedTypeSymbol symbol in symbols) {
            builder.GenerateSwappable(symbol);
        }
    }
}