using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S05 - a class went from public to open.</summary>
public class ClassSubclassingOffered : IEvaluateSwiftSignatures
{
    public string Rule => "ClassSubclassingOffered";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "became open, so it can now be subclassed";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (typePair.Older.AccessLevel == SwiftAccessLevels.Open)
            {
                continue;
            }

            if (typePair.Newer.AccessLevel != SwiftAccessLevels.Open)
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }
    }
}
