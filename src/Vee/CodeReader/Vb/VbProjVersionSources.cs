using Winterborn.Tools.Vee.CodeReader.Csharp;
using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;
using Winterborn.Tools.Vee.Providers;

namespace Winterborn.Tools.Vee.CodeReader.Vb;

/// <summary>
/// MVR-03 for .vbproj. The file is MSBuild, and <c>AssemblyVersion</c>, <c>PackageVersion</c> and
/// <c>FileVersion</c> mean there exactly what they mean in a .csproj, so this registers
/// <see cref="CsProjVersionSource"/> against VB rather than restating it.
/// <para>
/// It is a separate registration rather than widening the C# one's <c>UnitKinds</c> because
/// <see cref="VersionSourceFactories"/> matches on language first: a factory claiming "csharp"
/// never fires for a VB unit, and a factory claiming both languages could not exist without the
/// registry growing a concept of language groups for one caller.
/// </para>
/// </summary>
internal class VbProjVersionSources : IDiscoverVersionSources
{
    public string LanguageId => VbLanguageProvider.VbLanguageId;

    public IReadOnlyList<string> UnitKinds => [VbLanguageProvider.VbprojUnitKind];

    public IEnumerable<IVersionSource> Discover(VersionSourceScope scope)
    {
        yield return new CsProjVersionSource(
            scope.UnitPath,
            FolderScanner.GetRelativePath(scope.FolderRoot, scope.UnitPath));
    }
}
