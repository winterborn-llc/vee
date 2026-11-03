using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S35 - a property's setter is gone, so it is get-only now.</summary>
public class PropertySetterRemoved : IEvaluateSwiftSignatures
{
    public string Rule => "PropertySetterRemoved";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "lost its setter";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var propertyPair in SwiftMembers.GetPairedProperties(signatures))
        {
            if (!propertyPair.Older.IsSettable || propertyPair.Newer.IsSettable)
            {
                continue;
            }

            yield return propertyPair.Newer.Name;
        }
    }
}
