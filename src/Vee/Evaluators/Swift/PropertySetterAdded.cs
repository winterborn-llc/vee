using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S36 - a property gained a setter.</summary>
public class PropertySetterAdded : IEvaluateSwiftSignatures
{
    public string Rule => "PropertySetterAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "gained a setter";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var propertyPair in SwiftMembers.GetPairedProperties(signatures))
        {
            if (propertyPair.Older.IsSettable || !propertyPair.Newer.IsSettable)
            {
                continue;
            }

            yield return propertyPair.Newer.Name;
        }
    }
}
