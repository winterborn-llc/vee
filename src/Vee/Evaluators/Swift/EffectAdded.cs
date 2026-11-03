using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S23 - throws or async was added to an existing declaration, so every call site has to change.</summary>
public class EffectAdded : IEvaluateSwiftSignatures
{
    public string Rule => "EffectAdded";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "gained a throws or async effect";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var functionPair in SwiftMembers.GetPairedFunctions(signatures))
        {
            // Gaining both effects at once is still one thing that happened to the function.
            if (!functionPair.Older.Throws && functionPair.Newer.Throws)
            {
                yield return functionPair.Newer.Name;
                continue;
            }

            if (!functionPair.Older.IsAsync && functionPair.Newer.IsAsync)
            {
                yield return functionPair.Newer.Name;
            }
        }
    }
}
