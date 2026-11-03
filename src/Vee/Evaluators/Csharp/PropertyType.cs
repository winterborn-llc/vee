using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R13 - a property's type changed.</summary>
[DebuggerDisplay("{EvaluationImpact}")]
public class PropertyType : IEvaluateCsharpSignatures
{
    public string Rule => "PropertyType";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "changed its type";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var pair in Properties.GetPaired(signatures))
        {
            if (pair.Older.Type == pair.Newer.Type)
            {
                continue;
            }

            yield return $"{pair.DeclaringType.Name}.{pair.Newer.Name}";
        }
    }
}
