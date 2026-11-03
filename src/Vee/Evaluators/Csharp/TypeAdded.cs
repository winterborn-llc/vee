using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Csharp;
using Winterborn.Tools.Vee.Evaluation.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R19 - a public interface, struct, record, enum or delegate appeared.</summary>
public class TypeAdded : IEvaluateCsharpSignatures
{
    public string Rule => "TypeAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var newerType in signatures.Newer.Types)
        {
            if (newerType.Kind == CsharpTypeKinds.Class)
            {
                continue;
            }

            var olderType = CsharpSignaturesToCompare.FindType(
                signatures.Older, newerType.Name, newerType.Kind);
            if (olderType != null)
            {
                continue;
            }

            yield return newerType.Name;
        }
    }
}
