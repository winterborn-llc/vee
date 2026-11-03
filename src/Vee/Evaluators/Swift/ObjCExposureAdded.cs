using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S28 - ObjC exposure was added.</summary>
public class ObjCExposureAdded : IEvaluateSwiftSignatures
{
    public string Rule => "ObjCExposureAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "gained Objective-C exposure";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var pair in SwiftMembers.GetPairedDeclarations(signatures))
        {
            if (pair.Older.ObjCExposure.Length > 0)
            {
                continue;
            }

            if (pair.Newer.ObjCExposure.Length < 1)
            {
                continue;
            }

            yield return pair.Newer.Name;
        }
    }
}
