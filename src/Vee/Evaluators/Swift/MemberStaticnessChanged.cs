using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S34 - a member moved between static and instance, in either direction.</summary>
public class MemberStaticnessChanged : IEvaluateSwiftSignatures
{
    public string Rule => "MemberStaticnessChanged";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "moved between static and instance";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var functionPair in SwiftMembers.GetPairedFunctions(signatures))
        {
            if (functionPair.Older.IsStatic == functionPair.Newer.IsStatic)
            {
                continue;
            }

            yield return functionPair.Newer.Name;
        }

        foreach (var propertyPair in SwiftMembers.GetPairedProperties(signatures))
        {
            if (propertyPair.Older.IsStatic == propertyPair.Newer.IsStatic)
            {
                continue;
            }

            yield return propertyPair.Newer.Name;
        }
    }
}
