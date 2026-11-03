using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces;

namespace Winterborn.Tools.Vee.Providers;

/// <summary>
/// Maven projects, marked by a pom.xml (LNG-01, version-sync).
/// <para>
/// Gradle is deliberately absent, and not only because build.gradle and build.gradle.kts are
/// executable Groovy and Kotlin. A Gradle module does not say which language it is - Java, Kotlin
/// and Groovy share the build system and often the same module - so there is no honest language id
/// to file it under. Maven has the same ambiguity in principle and not in practice; Gradle is
/// recorded as an open question rather than guessed at.
/// </para>
/// </summary>
internal class JavaLanguageProvider(
    IReadOnlyList<IDiscoverVersionSources> versionSources)
    : ManifestLanguageProvider(versionSources)
{
    internal const string JavaLanguageId = "java";

    internal const string JavaUnitKind = "maven-project";

    /// <summary>FLD-06. Maven's build directory, vouched for by the pom beside it.</summary>
    public override IReadOnlyList<DirectoryExclusion> DirectoryExclusions =>
        [DirectoryExclusion.Beside("target", "pom.xml")];

    public override string LanguageId => JavaLanguageId;

    protected override string UnitKind => JavaUnitKind;

    protected override string ManifestFileName => "pom.xml";
}
