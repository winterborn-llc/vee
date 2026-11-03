using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>
/// R21 - an interface gained a requirement that carries a default implementation. Existing
/// implementers keep compiling, so this is additive.
/// </summary>
public class InterfaceRequirementAddedWithDefault : IEvaluateCsharpSignatures
{
    public string Rule => "InterfaceRequirementAddedWithDefault";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added as an interface requirement with a default";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        return InterfaceRequirements.GetAddedRequirements(
            signatures, withDefaultImplementation: true);
    }
}
