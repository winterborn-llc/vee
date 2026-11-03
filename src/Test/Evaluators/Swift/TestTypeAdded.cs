using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluators.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;
using Test.Swift;

namespace Test.Evaluators.Swift;

/// <summary>S02.</summary>
public class TestSwiftTypeAdded
{
    private static IEvaluateSwiftSignatures Evaluator => new TypeAdded();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Minor, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void TypesAreUnchanged()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Module(BuildSwift.Struct("Point")),
            BuildSwift.Module(BuildSwift.Struct("Point")));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void TypeIsAdded()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Module(BuildSwift.Struct("Point")),
            BuildSwift.Module(BuildSwift.Struct("Point"), BuildSwift.Actor("Counter")));

        Assert.Equal(["Counter"], Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void RemovingATypeDoesNotFire()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Module(BuildSwift.Struct("Point"), BuildSwift.Actor("Counter")),
            BuildSwift.Module(BuildSwift.Struct("Point")));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }
}
