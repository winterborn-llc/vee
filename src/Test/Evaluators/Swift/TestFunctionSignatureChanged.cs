using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluators.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;
using Test.Swift;

namespace Test.Evaluators.Swift;

/// <summary>S22.</summary>
public class TestSwiftFunctionSignatureChanged
{
    private static IEvaluateSwiftSignatures Evaluator => new FunctionSignatureChanged();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Major, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void SignatureIsUnchanged()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithFunctions(BuildSwift.Function().WithParameters(BuildSwift.Parameter())),
            BuildSwift.Struct().WithFunctions(BuildSwift.Function().WithParameters(BuildSwift.Parameter())));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void ReturnTypeChanged()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithFunctions(BuildSwift.Function(returns: "Int")),
            BuildSwift.Struct().WithFunctions(BuildSwift.Function(returns: "String")));

        Assert.Equal(["TestType.move()"], Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void ParameterTypeChanged()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithFunctions(BuildSwift.Function().WithParameters(BuildSwift.Parameter(type: "Int"))),
            BuildSwift.Struct().WithFunctions(BuildSwift.Function().WithParameters(BuildSwift.Parameter(type: "Double"))));

        Assert.NotEmpty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void ParameterCountChanged()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithFunctions(BuildSwift.Function().WithParameters(BuildSwift.Parameter())),
            BuildSwift.Struct().WithFunctions(BuildSwift.Function()
                .WithParameters(BuildSwift.Parameter(), BuildSwift.Parameter("animated", "Bool"))));

        Assert.NotEmpty(Evaluator.FindDifferences(signatures));
    }
}
