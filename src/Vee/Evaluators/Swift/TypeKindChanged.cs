using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S03 - a type changed kind: struct to class, enum to struct, and so on.</summary>
public class TypeKindChanged : IEvaluateSwiftSignatures
{
    public string Rule => "TypeKindChanged";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "changed kind";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (typePair.Older.Kind == typePair.Newer.Kind)
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }
    }
}
