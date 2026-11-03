using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;
using Winterborn.Tools.Vee.Providers;

namespace Winterborn.Tools.Vee.CodeReader.Csharp;

/// <summary>MVR-03 - the version properties in the project file the unit already is.</summary>
internal class CsProjVersionSources : IDiscoverVersionSources
{
    public string LanguageId => CsharpLanguageProvider.CsharpLanguageId;

    public IReadOnlyList<string> UnitKinds => [CsharpLanguageProvider.CsprojUnitKind];

    public IEnumerable<IVersionSource> Discover(VersionSourceScope scope)
    {
        yield return new CsProjVersionSource(
            scope.UnitPath,
            FolderScanner.GetRelativePath(scope.FolderRoot, scope.UnitPath));
    }
}
