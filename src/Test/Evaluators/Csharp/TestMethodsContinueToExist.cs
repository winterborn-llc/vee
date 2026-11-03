using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluators.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Test.Evaluators.Csharp;

/// <summary>R01.</summary>
public class TestMethodsContinueToExist
{
    private static IEvaluateCsharpSignatures Evaluator => new MethodsContinueToExist();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Major, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void MethodsAreUnchanged()
    {
        var signatures = Build.Compare(
            Build.Class().WithMethods(Build.Method("One"), Build.Method("Two")),
            Build.Class().WithMethods(Build.Method("One"), Build.Method("Two")));

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void MethodNoLongerExists()
    {
        var signatures = Build.Compare(
            Build.Class().WithMethods(Build.Method("One"), Build.Method("Two")),
            Build.Class().WithMethods(Build.Method("One"), Build.Method("Three")));

        // The rule names what it found, which is what a dry run reports (§20 O-04).
        Assert.Equal(["Test.TestType.Two"], Evaluator.FindDifferences(signatures));
    }
}
