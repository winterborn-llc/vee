using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S30 - mutating was removed.</summary>
public class MutatingRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "MutatingRemoved";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "is no longer mutating";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var functionPair in SwiftMembers.GetPairedFunctions(signatures))
        {
            if (!functionPair.Older.IsMutating || functionPair.Newer.IsMutating)
            {
                continue;
            }

            yield return functionPair.Newer.Name;
        }
    }
}
