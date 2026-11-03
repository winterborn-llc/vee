using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;
using Winterborn.Tools.Vee.Providers;

namespace Winterborn.Tools.Vee.CodeReader.Swift;

/// <summary>MVR-03/MVR-04 - MARKETING_VERSION in the project file inside the .xcodeproj.</summary>
internal class MarketingVersionSources : IDiscoverVersionSources
{
    internal const string XcodeProjectFileName = "project.pbxproj";

    public string LanguageId => SwiftLanguageProvider.SwiftLanguageId;

    public IReadOnlyList<string> UnitKinds => [SwiftLanguageProvider.XcodeTargetUnitKind];

    public IEnumerable<IVersionSource> Discover(VersionSourceScope scope)
    {
        var path = Path.Combine(scope.UnitPath, XcodeProjectFileName);
        if (!File.Exists(path) || !MarketingVersionSource.HasLiteralVersion(File.ReadAllText(path)))
        {
            yield break;
        }

        yield return new MarketingVersionSource(
            path,
            FolderScanner.GetRelativePath(scope.FolderRoot, path));
    }
}
