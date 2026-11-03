using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;
using Winterborn.Tools.Vee.Providers;

namespace Winterborn.Tools.Vee.CodeReader.Swift;

/// <summary>MVR-03/MVR-04 - a literal version in a podspec beside the package manifest.</summary>
internal class PodspecVersionSources : IDiscoverVersionSources
{
    public string LanguageId => SwiftLanguageProvider.SwiftLanguageId;

    public IReadOnlyList<string> UnitKinds => [SwiftLanguageProvider.SwiftPackageTargetUnitKind];

    public IEnumerable<IVersionSource> Discover(VersionSourceScope scope)
    {
        foreach (var path in FolderScanner.FindFiles(scope.UnitPath, "*.podspec"))
        {
            if (!PodspecVersionSource.HasLiteralVersion(File.ReadAllText(path)))
            {
                continue;
            }

            yield return new PodspecVersionSource(
                path,
                FolderScanner.GetRelativePath(scope.FolderRoot, path));
        }
    }
}
