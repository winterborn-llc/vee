using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluators.Swift;
using Winterborn.Tools.Vee.Interfaces.Swift;
using Test.Swift;

namespace Test.Evaluators.Swift;

/// <summary>S04 - directional against S05.</summary>
public class TestSwiftClassSubclassingWithdrawn
{
    private static IEvaluateSwiftSignatures Evaluator => new ClassSubclassingWithdrawn();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Major, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void AccessLevelIsUnchanged()
    {
        Assert.Empty(Evaluator.FindDifferences(
            BuildSwift.Compare(BuildSwift.Class(accessLevel: "open"), BuildSwift.Class(accessLevel: "open"))));
    }

    [Fact]
    public void OpenBecamePublic()
    {
        Assert.Equal(
            [BuildSwift.DefaultTypeName],
            Evaluator.FindDifferences(
                BuildSwift.Compare(BuildSwift.Class(accessLevel: "open"), BuildSwift.Class())));
    }

    [Fact]
    public void PublicBecomingOpenDoesNotFire()
    {
        Assert.Empty(Evaluator.FindDifferences(
            BuildSwift.Compare(BuildSwift.Class(), BuildSwift.Class(accessLevel: "open"))));
    }
}
