using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;
using Winterborn.Tools.Vee.Providers;

namespace Winterborn.Tools.Vee.CodeReader.Manifests;

/// <summary>MVR-03/MVR-04 - the project's own version in a pom.xml, and never a dependency's.</summary>
internal class PomVersionSources : IDiscoverVersionSources
{
    public string LanguageId => JavaLanguageProvider.JavaLanguageId;

    public IReadOnlyList<string> UnitKinds => [JavaLanguageProvider.JavaUnitKind];

    public IEnumerable<IVersionSource> Discover(VersionSourceScope scope)
    {
        if (!File.Exists(scope.UnitPath))
        {
            yield break;
        }

        string text;
        try
        {
            text = File.ReadAllText(scope.UnitPath);
        }
        catch (IOException e)
        {
            Log.WriteLine($"Could not read {scope.UnitPath}: {e.Message}");
            yield break;
        }

        if (!PomVersionSource.HasOwnVersion(text))
        {
            yield break;
        }

        yield return new PomVersionSource(
            scope.UnitPath,
            FolderScanner.GetRelativePath(scope.FolderRoot, scope.UnitPath));
    }
}
