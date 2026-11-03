using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R09 - a property lost its setter.</summary>
public class PropertyEditabilityReduced : IEvaluateCsharpSignatures
{
    public string Rule => "PropertyEditabilityReduced";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "is no longer writable";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var pair in Properties.GetPaired(signatures))
        {
            if (pair.Newer.IsWritable)
            {
                continue;
            }

            if (!pair.Older.IsWritable)
            {
                continue;
            }

            yield return $"{pair.DeclaringType.Name}.{pair.Newer.Name}";
        }
    }
}
