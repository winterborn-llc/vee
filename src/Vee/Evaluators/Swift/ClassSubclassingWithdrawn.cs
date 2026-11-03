using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S04 - a class went from open to public, withdrawing subclassing and overriding.</summary>
public class ClassSubclassingWithdrawn : IEvaluateSwiftSignatures
{
    public string Rule => "ClassSubclassingWithdrawn";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "is no longer open, so it can no longer be subclassed";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (typePair.Older.AccessLevel != SwiftAccessLevels.Open)
            {
                continue;
            }

            if (typePair.Newer.AccessLevel == SwiftAccessLevels.Open)
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }
    }
}
