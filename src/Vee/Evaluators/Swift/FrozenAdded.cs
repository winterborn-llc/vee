using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S15 - @frozen was added, which only promises callers more.</summary>
public class FrozenAdded : IEvaluateSwiftSignatures
{
    public string Rule => "FrozenAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "became @frozen";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (typePair.Older.IsFrozen || !typePair.Newer.IsFrozen)
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }
    }
}
