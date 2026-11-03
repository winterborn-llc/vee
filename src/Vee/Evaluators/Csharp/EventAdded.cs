using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R31 - a public event appeared on an existing type.</summary>
public class EventAdded : IEvaluateCsharpSignatures
{
    public string Rule => "EventAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.ClassHistory)
        {
            foreach (var newerEvent in typePair.Newer.Events)
            {
                if (Events.Find(typePair.Older, newerEvent.Name) != null)
                {
                    continue;
                }

                yield return $"{typePair.Newer.Name}.{newerEvent.Name}";
            }
        }
    }
}
