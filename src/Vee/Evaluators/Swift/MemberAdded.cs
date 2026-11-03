using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S17 - a public member was added to an existing type.</summary>
public class MemberAdded : IEvaluateSwiftSignatures
{
    public string Rule => "MemberAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            foreach (var newerMember in SwiftMembers.GetAll(typePair.Newer))
            {
                if (SwiftMembers.Find(typePair.Older, newerMember) != null)
                {
                    continue;
                }

                yield return newerMember.Name;
            }
        }
    }
}
