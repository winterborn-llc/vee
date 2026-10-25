using System.Text.Json.Serialization;
using Winterborn.Tools.EasySemVer.Evaluation;

namespace Winterborn.Tools.EasySemVer.Reporting;

/// <summary>
/// The shape of the <c>--json</c> report (REP-02). Deliberately small: discovered units were
/// weighed and left out, because they belong in the log and no consumer for them exists. REP-04
/// makes adding a field backwards-compatible, so this can grow into whatever a real consumer turns
/// out to need - which is what makes starting minimal safe rather than short-sighted, and is
/// exactly how <see cref="Findings"/> (REP-09) and then <see cref="WrittenFiles"/> (REP-10) both
/// arrived later without a format version bump.
/// <para>
/// Property order is pinned rather than left to reflection, because REP-07 requires two runs over
/// unchanged source to produce byte-identical output.
/// </para>
/// </summary>
public class JsonReportDocument
{
    /// <summary>
    /// REP-03. Versioned independently of the baseline's own format version: the two documents
    /// have different audiences and different failure modes.
    /// <para>
    /// Bumped to 2 when a finding's <c>ruleId</c> ("R02") became a <c>rule</c> named within its
    /// language ("MethodReturnType"). REP-04 makes an added field backwards-compatible, which is
    /// why <see cref="Findings"/> and <see cref="WrittenFiles"/> both arrived without a bump; a
    /// renamed one is not, and a consumer matching on the old key has to be told.
    /// </para>
    /// </summary>
    [JsonPropertyName("formatVersion")]
    [JsonPropertyOrder(0)]
    public int FormatVersion { get; init; } = 2;

    /// <summary>The only signal that a report describes a preview rather than something that happened.</summary>
    [JsonPropertyName("dryRun")]
    [JsonPropertyOrder(1)]
    public bool IsDryRun { get; init; }

    /// <summary>
    /// "major" | "minor" | "patch" - the increment this run applied. REP-06: stated, never
    /// inferred, because VER-05's overflow rollover makes deriving it from the two versions wrong
    /// in both directions. CLI-14 gives it a second reason: a caller can name the delta, and then
    /// this is what they named rather than anything the two versions could reveal.
    /// </summary>
    [JsonPropertyName("changeType")]
    [JsonPropertyOrder(2)]
    public string ChangeType { get; init; } = string.Empty;

    /// <summary>
    /// REP-11 - what the API surface said, as against what the version moved by. The two differ
    /// only on a run that was handed its delta (CLI-14), and this is the one field that keeps the
    /// classification when it was: <see cref="ChangeType"/> has gone over to the instruction, and
    /// <see cref="Findings"/> carries the evidence but not the verdict it adds up to.
    /// <para>
    /// Always present, equal to <see cref="ChangeType"/> on an ordinary run. Absent-when-equal
    /// would make "nothing overrode the verdict" and "an older writer" the same observation, which
    /// is the reasoning <see cref="Findings"/> and <see cref="WrittenFiles"/> are present-and-empty
    /// for.
    /// </para>
    /// </summary>
    [JsonPropertyName("detectedChangeType")]
    [JsonPropertyOrder(3)]
    public string DetectedChangeType { get; init; } = string.Empty;

    [JsonPropertyName("oldVersion")]
    [JsonPropertyOrder(4)]
    public JsonReportVersion OldVersion { get; init; } = new();

    [JsonPropertyName("newVersion")]
    [JsonPropertyOrder(5)]
    public JsonReportVersion NewVersion { get; init; } = new();

    /// <summary>
    /// REP-09 - the evidence behind the verdict, in the order <see cref="ChangeReport"/> already
    /// sorts it. Present and empty rather than absent when a run found nothing: an absent array
    /// would make "no changes" and "an older writer" indistinguishable.
    /// </summary>
    [JsonPropertyName("findings")]
    [JsonPropertyOrder(6)]
    public IReadOnlyList<JsonReportFinding> Findings { get; init; } = [];

    /// <summary>
    /// REP-10 - every file the run changed, folder-root-relative with forward slashes, sorted and
    /// deduplicated. This is what a workflow stages: the alternative is a hand-maintained path list
    /// in someone's YAML, which goes stale silently the first time a project moves and produces a
    /// green run that tagged a commit without the bump in it.
    /// <para>
    /// Present and empty on a dry run rather than absent, for the same reason as
    /// <see cref="Findings"/>: "wrote nothing" and "an older writer" must never be the same
    /// observation.
    /// </para>
    /// </summary>
    [JsonPropertyName("writtenFiles")]
    [JsonPropertyOrder(7)]
    public IReadOnlyList<string> WrittenFiles { get; init; } = [];
}
