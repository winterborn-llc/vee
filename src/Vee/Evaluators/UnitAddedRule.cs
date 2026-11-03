using Winterborn.Tools.Vee.DataObject;
using Winterborn.Tools.Vee.Evaluation;
using Winterborn.Tools.Vee.Interfaces;

namespace Winterborn.Tools.Vee.Evaluators;

/// <summary>
/// "A unit exists this run that the baseline never saw", and what makes a first run Minor
/// (BAS-05). Owned per language for the same reason as <see cref="UnitRemovedRule"/>.
/// </summary>
public abstract class UnitAddedRule : IEvaluateUnitExistence
{
    /// <inheritdoc cref="UnitRemovedRule.Rule"/>
    public abstract string Rule { get; }

    public virtual VersionType EvaluationImpact => VersionType.Minor;

    public virtual string ChangeDescription => "was added";

    public virtual IEnumerable<IPackageableUnit> FindDifferences(IUnitsToCompare units)
    {
        foreach (var newerUnit in units.Newer)
        {
            if (UnitPairing.Find(units.Older, newerUnit) != null)
            {
                continue;
            }

            yield return newerUnit;
        }
    }
}
