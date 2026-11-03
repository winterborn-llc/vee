using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluation.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.Evaluators.Swift;

/// <summary>S11 - a generic parameter count changed.</summary>
public class GenericParameterCountChanged : IEvaluateSwiftSignatures
{
    public string Rule => "GenericParameterCountChanged";

    public VersionType EvaluationImpact => VersionType.Major;

    public string ChangeDescription => "changed its number of generic parameters";

    public IEnumerable<string> FindDifferences(ISwiftSignaturesToCompare signatures)
    {
        foreach (var typePair in signatures.TypeHistory)
        {
            if (!SwiftGenericConstraints.DidCountChange(
                    typePair.Older.GenericParameters,
                    typePair.Newer.GenericParameters))
            {
                continue;
            }

            yield return typePair.Newer.Name;
        }

        foreach (var functionPair in SwiftMembers.GetPairedFunctions(signatures))
        {
            if (!SwiftGenericConstraints.DidCountChange(
                    functionPair.Older.GenericParameters,
                    functionPair.Newer.GenericParameters))
            {
                continue;
            }

            yield return functionPair.Newer.Name;
        }
    }
}
