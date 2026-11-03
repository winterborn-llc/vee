using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.DataObject.Swift;
using Winterborn.Tools.Vee.Evaluators.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;
using Test.Swift;

namespace Test.Evaluators.Swift;

/// <summary>S10 - directional against S09.</summary>
public class TestSwiftConformanceAdded
{
    private static IEvaluateSwiftSignatures Evaluator => new ConformanceAdded();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Minor, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void ConformancesAreUnchanged()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithConformances("Equatable"),
            BuildSwift.Struct().WithConformances("Equatable"));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void ConformanceIsAdded()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithConformances("Equatable"),
            BuildSwift.Struct().WithConformances("Equatable", "Hashable"));

        Assert.Equal(["TestType (Hashable)"], Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void RemovingAConformanceDoesNotFire()
    {
        var signatures = BuildSwift.Compare(
            BuildSwift.Struct().WithConformances("Equatable", "Hashable"),
            BuildSwift.Struct().WithConformances("Equatable"));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }
}
