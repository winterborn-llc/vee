using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

/// <summary>
/// R37 - a parameter's ref, out, in or params modifier changed. The call site has to change with
/// it, so this is breaking in either direction.
/// </summary>
public class ParameterModifierChanged : IEvaluateCsharpSignatures
{
    public string Rule => "ParameterModifierChanged";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "changed a parameter modifier";

    public IEnumerable<string> FindDifferences(ICsharpSignaturesToCompare signatures)
    {
        foreach (var overloadPair in Overloads.GetMatchedOverloads(signatures))
        {
            if (!DidAnyModifierChange(overloadPair))
            {
                continue;
            }

            yield return overloadPair.Symbol;
        }
    }

    private static bool DidAnyModifierChange(Overloads.OverloadPair overloadPair)
    {
        var older = overloadPair.Older.Parameters;
        var newer = overloadPair.Newer.Parameters;
        for (var i = 0; i < older.Count; i++)
        {
            if (older[i].RefKind != newer[i].RefKind)
            {
                return true;
            }

            if (older[i].IsParams != newer[i].IsParams)
            {
                return true;
            }
        }

        return false;
    }
}
