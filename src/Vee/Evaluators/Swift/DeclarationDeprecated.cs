using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S26 - a declaration was marked deprecated. Nothing stops compiling, so on its own this is a Patch; anything else that changed alongside it will out-rank it (CLS-03).</summary>
public class DeclarationDeprecated : IEvaluateSwiftSignatures
{
    public string Rule => "DeclarationDeprecated";

    public VersionType EvaluationImpact => VersionType.Patch;

    public string ChangeDescription => "was marked deprecated";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var pair in SwiftMembers.GetPairedDeclarations(signatures))
        {
            if (SwiftAvailabilityFacts.IsDeprecated(pair.Older))
            {
                continue;
            }

            if (!SwiftAvailabilityFacts.IsDeprecated(pair.Newer))
            {
                continue;
            }

            yield return pair.Newer.Name;
        }
    }
}
