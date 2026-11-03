using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S14 - @frozen was removed from a public struct or enum, so its layout is no longer guaranteed.</summary>
public class FrozenRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "FrozenRemoved";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "is no longer @frozen";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (!typePair.Older.IsFrozen || typePair.Newer.IsFrozen)
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }
    }
}
