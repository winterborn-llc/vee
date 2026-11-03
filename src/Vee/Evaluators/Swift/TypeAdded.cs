using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S02 - a public type appeared.</summary>
public class TypeAdded : IEvaluateSwiftSignatures
{
    public string Rule => "TypeAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var newerType in signatures.Newer.Types)
        {
            if (SwiftSignaturesToCompare.FindType(signatures.Older, newerType.Name) != null)
            {
                continue;
            }

            yield return newerType.Name;
        }
    }
}
