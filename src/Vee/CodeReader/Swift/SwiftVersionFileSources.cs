using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;
using Winterborn.Tools.Vee.Providers;

namespace Winterborn.Tools.Vee.CodeReader.Swift;

/// <summary>MVR-03/MVR-04 - a version constant declared in Swift source.</summary>
internal class SwiftVersionFileSources : IDiscoverVersionSources
{
    public string LanguageId => SwiftLanguageProvider.SwiftLanguageId;

    public IReadOnlyList<string> UnitKinds => [SwiftLanguageProvider.SwiftPackageTargetUnitKind];

    public IEnumerable<IVersionSource> Discover(VersionSourceScope scope)
    {
        foreach (var path in FolderScanner.FindFiles(scope.UnitPath, "*Version.swift"))
        {
            if (!SwiftVersionFileSource.HasVersionConstant(File.ReadAllText(path)))
            {
                continue;
            }

            yield return new SwiftVersionFileSource(
                path,
                FolderScanner.GetRelativePath(scope.FolderRoot, path));
        }
    }
}
