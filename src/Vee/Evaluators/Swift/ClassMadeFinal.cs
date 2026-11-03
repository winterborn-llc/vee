using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S06 - a class gained final, so existing subclasses stop compiling.</summary>
public class ClassMadeFinal : IEvaluateSwiftSignatures
{
    public string Rule => "ClassMadeFinal";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "became final";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (typePair.Older.IsFinal || !typePair.Newer.IsFinal)
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }
    }
}
