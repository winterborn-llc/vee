using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>
/// R24 - a member kept its name but changed value. Anything already compiled against the old
/// constant, or persisted using it, is now wrong.
/// </summary>
public class EnumMemberValueChanged : IEvaluateCsharpSignatures
{
    public string Rule => "EnumMemberValueChanged";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "changed its value";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var pair in EnumMembers.GetPairedMembers(signatures))
        {
            if (pair.Older.Value == pair.Newer.Value)
            {
                continue;
            }

            yield return $"{pair.DeclaringEnum.Name}.{pair.Newer.Name}";
        }
    }
}
