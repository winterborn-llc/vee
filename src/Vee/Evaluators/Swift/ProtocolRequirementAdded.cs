using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S20 - a protocol gained a requirement with no default implementation, so every conformer stops compiling.</summary>
public class ProtocolRequirementAdded : IEvaluateSwiftSignatures
{
    public string Rule => "ProtocolRequirementAdded";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "was added as a protocol requirement with no default";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        return SwiftProtocolRequirements.GetAddedRequirements(
            signatures, withDefaultImplementation: false);
    }
}
