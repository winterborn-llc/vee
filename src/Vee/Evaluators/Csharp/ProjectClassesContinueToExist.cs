using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>R06 - a public class the baseline recorded is gone, renamed, or moved namespace.</summary>
public class ProjectClassesContinueToExist : IEvaluateCsharpSignatures
{
    public string Rule => "ProjectClassesContinueToExist";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "was removed";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var olderClass in signatures.Older.Classes)
        {
            var newerClass = CsharpSignaturesToCompare.FindType(
                signatures.Newer, olderClass.Name, olderClass.Kind);
            if (newerClass != null)
            {
                continue;
            }

            yield return olderClass.Name;
        }
    }
}
