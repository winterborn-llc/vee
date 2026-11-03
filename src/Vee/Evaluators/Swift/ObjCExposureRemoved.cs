using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S27 - ObjC exposure was removed from a public declaration, breaking Objective-C and KVO clients (SWM-04).</summary>
public class ObjCExposureRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "ObjCExposureRemoved";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "lost or changed its Objective-C exposure";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var pair in SwiftMembers.GetPairedDeclarations(signatures))
        {
            if (pair.Older.ObjCExposure.Length < 1)
            {
                continue;
            }

            if (pair.Newer.ObjCExposure == pair.Older.ObjCExposure)
            {
                continue;
            }

            yield return pair.Newer.Name;
        }
    }
}
