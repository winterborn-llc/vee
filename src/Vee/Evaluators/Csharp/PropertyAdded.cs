using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R16 - a type that exists on both sides gained a property.</summary>
public class PropertyAdded : IEvaluateCsharpSignatures
{
    public string Rule => "PropertyAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var classPair in signatures.ClassHistory)
        {
            foreach (var newPropertyName in classPair.Newer.Properties.Keys)
            {
                if (classPair.Older.Properties.Contains(newPropertyName))
                {
                    continue;
                }

                yield return $"{classPair.Newer.Name}.{newPropertyName}";
            }
        }
    }
}
