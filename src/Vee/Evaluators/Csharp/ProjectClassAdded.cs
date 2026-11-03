using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R05 - the unit gained a public class the baseline did not have.</summary>
public class ProjectClassAdded : IEvaluateCsharpSignatures
{
    public string Rule => "ProjectClassAdded";

    public VersionType EvaluationImpact => VersionType.Minor;

    public string ChangeDescription => "was added";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var newerClass in signatures.Newer.Classes)
        {
            var olderClass = CsharpSignaturesToCompare.FindType(
                signatures.Older, newerClass.Name, newerClass.Kind);
            if (olderClass != null)
            {
                continue;
            }

            yield return newerClass.Name;
        }
    }
}
