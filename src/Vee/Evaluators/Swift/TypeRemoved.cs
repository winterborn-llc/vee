using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S01 - a public type is gone from the module.</summary>
public class TypeRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "TypeRemoved";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "was removed";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var olderType in signatures.Older.Types)
        {
            if (SwiftSignaturesToCompare.FindType(signatures.Newer, olderType.Name) != null)
            {
                continue;
            }

            yield return olderType.Name;
        }
    }
}
