using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R41 (addition half) - a public nested type appeared.</summary>
public class NestedTypeAdded : IEvaluateCsharpSignatures
{
    public string Rule => "NestedTypeAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var newerType in signatures.Newer.Types)
        {
            if (newerType.DeclaringType.Length < 1)
            {
                continue;
            }

            var olderType = CsharpSignaturesToCompare.FindTypeOfAnyKind(signatures.Older, newerType.Name);
            if (olderType != null)
            {
                continue;
            }

            yield return newerType.Name;
        }
    }
}
