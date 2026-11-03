using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S18 - an enum case was added. Major, not Minor: a client switching exhaustively stops compiling, which is every client of a package built without library evolution (SCL-01).</summary>
public class EnumCaseAdded : IEvaluateSwiftSignatures
{
    public string Rule => "EnumCaseAdded";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "was added, so an exhaustive switch no longer compiles";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var enumPair in SwiftEnums.GetPaired(signatures))
        {
            foreach (var newerCase in enumPair.Newer.Cases)
            {
                if (SwiftMembers.FindCase(enumPair.Older, newerCase.Name) != null)
                {
                    continue;
                }

                yield return newerCase.Name;
            }
        }
    }
}
