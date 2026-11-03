using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluators.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Test.Evaluators.Csharp;

/// <summary>R11 - a property became readable.</summary>
public class TestPropertyReadabilityEnhanced
{
    private static IEvaluateCsharpSignatures Evaluator => new PropertyReadabilityEnhanced();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Minor, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void AccessorIsUnchanged()
    {
        Assert.Empty(Evaluator.FindDifferences(BuildComparison(true, true)));
    }

    [Fact]
    public void AccessorChangesInTheFiringDirection()
    {
        Assert.Equal(
            ["Test.TestType.TestProperty"],
            Evaluator.FindDifferences(BuildComparison(false, true)));
    }

    [Fact]
    public void TheOppositeDirectionDoesNotFire()
    {
        Assert.Empty(Evaluator.FindDifferences(BuildComparison(true, false)));
    }

    private static ICsharpSignaturesToCompare BuildComparison(bool wasPresent, bool isPresent)
    {
        return Build.Compare(
            Build.Class().WithProperties(Build.Property(isReadable: wasPresent)),
            Build.Class().WithProperties(Build.Property(isReadable: isPresent)));
    }
}
