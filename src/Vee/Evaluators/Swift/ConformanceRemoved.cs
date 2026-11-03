using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S09 - a protocol conformance was removed from a public type.</summary>
public class ConformanceRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "ConformanceRemoved";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "no longer conforms to a protocol it used to";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            foreach (var conformance in typePair.Older.Conformances)
            {
                if (typePair.Newer.Conformances.Contains(conformance))
                {
                    continue;
                }

                // The type is the subject; the protocol it dropped rides along in the symbol so
                // the line names it without the description having to vary per finding.
                yield return $"{typePair.Newer.Name} ({conformance})";
            }
        }
    }
}
