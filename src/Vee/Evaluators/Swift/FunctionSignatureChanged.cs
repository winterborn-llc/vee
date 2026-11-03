using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S22 - a function's parameter labels, types, order, count, or return type changed.</summary>
public class FunctionSignatureChanged : IEvaluateSwiftSignatures
{
    public string Rule => "FunctionSignatureChanged";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "changed its signature";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var functionPair in SwiftMembers.GetPairedFunctions(signatures))
        {
            // Parameters and return type are one signature, so a function that changed both is
            // still one finding.
            if (functionPair.Older.ReturnType == functionPair.Newer.ReturnType
                && SwiftParameters.AreTheSame(
                    functionPair.Older.Parameters, functionPair.Newer.Parameters))
            {
                continue;
            }

            yield return functionPair.Newer.Name;
        }
    }
}
