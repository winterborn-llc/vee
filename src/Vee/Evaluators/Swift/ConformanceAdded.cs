using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S10 - a protocol conformance was added.</summary>
public class ConformanceAdded : IEvaluateSwiftSignatures
{
    public string Rule => "ConformanceAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "conforms to a protocol it did not before";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            foreach (var conformance in typePair.Newer.Conformances)
            {
                if (typePair.Older.Conformances.Contains(conformance))
                {
                    continue;
                }

                yield return $"{typePair.Newer.Name} ({conformance})";
            }
        }
    }
}
