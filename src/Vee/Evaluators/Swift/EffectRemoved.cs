using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S24 - throws or async was removed, which existing call sites tolerate.</summary>
public class EffectRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "EffectRemoved";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "lost a throws or async effect";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var functionPair in SwiftMembers.GetPairedFunctions(signatures))
        {
            // Losing both effects at once is still one thing that happened to the function.
            if (functionPair.Older.Throws && !functionPair.Newer.Throws)
            {
                yield return functionPair.Newer.Name;
                continue;
            }

            if (functionPair.Older.IsAsync && !functionPair.Newer.IsAsync)
            {
                yield return functionPair.Newer.Name;
            }
        }
    }
}
