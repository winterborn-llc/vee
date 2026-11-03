using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluators.Csharp;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Test.Evaluators.Csharp;

/// <summary>R25.</summary>
public class TestEnumUnderlyingTypeChanged
{
    private static IEvaluateCsharpSignatures Evaluator => new EnumUnderlyingTypeChanged();

    [Fact]
    public void ChangeTypeIsExpected()
    {
        Assert.Equal(VersionType.Major, Evaluator.EvaluationImpact);
    }

    [Fact]
    public void UnderlyingTypeIsUnchanged()
    {
        var signatures = Build.Compare(Build.Enum(), Build.Enum());

        Assert.Empty(Evaluator.FindDifferences(signatures));
    }

    [Fact]
    public void UnderlyingTypeChanged()
    {
        var signatures = Build.Compare(
            Build.Enum(underlyingType: "int"),
            Build.Enum(underlyingType: "byte"));

        Assert.Equal(["Test.TestType"], Evaluator.FindDifferences(signatures));
    }
}
