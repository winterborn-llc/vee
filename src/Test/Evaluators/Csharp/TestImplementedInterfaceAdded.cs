using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluators.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Test.Evaluators.Csharp;

/// <summary>R35 - directional against R34.</summary>
public class TestImplementedInterfaceAdded
{
    private static IEvaluateCsharpSignatures Evaluator => new ImplementedInterfaceAdded();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Minor, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void InterfacesAreUnchanged()
    {
        var signatures = Build.Compare(
            Build.Class().WithInterfaces("Test.IThing"),
            Build.Class().WithInterfaces("Test.IThing"));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void InterfaceIsAdded()
    {
        var signatures = Build.Compare(
            Build.Class().WithInterfaces("Test.IThing"),
            Build.Class().WithInterfaces("Test.IThing", "Test.IOther"));

        Assert.Equal(["Test.TestType (Test.IOther)"], Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void RemovingAnInterfaceDoesNotFire()
    {
        var signatures = Build.Compare(
            Build.Class().WithInterfaces("Test.IThing", "Test.IOther"),
            Build.Class().WithInterfaces("Test.IThing"));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }
}
