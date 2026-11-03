using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S25 - a declaration became unavailable or gained an obsoleted availability.</summary>
public class DeclarationWithdrawn : IEvaluateSwiftSignatures
{
    public string Rule => "DeclarationWithdrawn";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "became unavailable or obsoleted";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var pair in SwiftMembers.GetPairedDeclarations(signatures))
        {
            if (SwiftAvailabilityFacts.IsWithdrawn(pair.Older))
            {
                continue;
            }

            if (!SwiftAvailabilityFacts.IsWithdrawn(pair.Newer))
            {
                continue;
            }

            yield return pair.Newer.Name;
        }
    }
}
