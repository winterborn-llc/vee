using System.Text.Json;
using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Version = Winterborn.Tools.Vee.DataObject.Version;

namespace Winterborn.Tools.Vee.Reporting;

/// <summary>
/// Writes the run's verdict as JSON (REP-01). A sibling of <see cref="TextChangeReport"/> and,
/// like it, a formatter and nothing else: it reads a <see cref="ChangeReport"/> that already
/// exists and never re-examines a signature.
/// <para>
/// The document goes to a file and never to stdout, so LOG-01 keeps stdout unconditionally and a
/// consumer never has to disentangle a report from a log.
/// </para>
/// </summary>
internal static class JsonChangeReport
{
    private static readonly JsonSerializerOptions Options = new()
    {
        // REP-07: two runs over unchanged source must produce byte-identical output. Property
        // order is pinned by JsonPropertyOrder rather than left to reflection for the same reason.
        WriteIndented = true
    };

    internal static JsonReportDocument Build(
        ChangeReport report,
        Version oldVersion,
        Version newVersion,
        bool isDryRun,
        IReadOnlyList<string>? writtenFiles = null,
        VersionType? instructedChange = null)
    {
        return new JsonReportDocument
        {
            IsDryRun = isDryRun,

            // CLI-14: the verdict the version moved by, which is the instruction when the caller
            // named one. A consumer reading this field acts on the bump, so it has to describe the
            // bump that happened rather than the one this run would have chosen unprompted.
            ChangeType = Name(instructedChange ?? report.ChangeType),

            // REP-11 - and what the API surface itself said, which is the only place that survives
            // an instruction.
            DetectedChangeType = Name(report.ChangeType),
            OldVersion = JsonReportVersion.From(oldVersion),
            NewVersion = JsonReportVersion.From(newVersion),

            // REP-09. The order is ChangeReport's, which is already the sorted one, so this
            // formatter stays a formatter: no re-sorting here, and REP-07 holds for free.
            Findings = [.. report.Findings.Select(JsonReportFinding.From)],

            // REP-10. Sorted and deduplicated here rather than left to the order the providers
            // happened to write in: two units can legitimately share a version location, and REP-07
            // wants one answer for one tree however discovery ordered it.
            WrittenFiles =
            [
                .. (writtenFiles ?? []).Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal)
            ]
        };
    }

    /// <summary>
    /// Lower case throughout: this is a wire format, not a dump of a C# enum, and it removes a
    /// class of casing bugs in shell and YAML comparisons.
    /// </summary>
    private static string Name(VersionType type)
    {
        return type.ToString().ToLowerInvariant();
    }

    internal static string Render(JsonReportDocument document)
    {
        // REP-07 again, and the reason this is not left to JsonSerializerOptions.NewLine: that
        // option only exists from .NET 9, and this assembly still targets net8.0. Normalising the
        // rendered text is both TFM-independent and machine-independent, so a report written on
        // Windows is byte-identical to one written on a Linux or macOS runner.
        return JsonSerializer.Serialize(document, Options).ReplaceLineEndings("\n");
    }

    internal static void Write(string path, JsonReportDocument document)
    {
        var directory = Path.GetDirectoryName(Path.GetFullPath(path));
        if (directory != null && !Directory.Exists(directory))
        {
            // A workflow writing to out/report.json should not have to mkdir first.
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, Render(document));
        Log.WriteLine($"Wrote JSON report {path}");
    }
}
