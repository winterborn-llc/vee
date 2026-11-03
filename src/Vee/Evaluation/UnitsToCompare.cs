using Winterborn.Tools.Vee.Interfaces;

namespace Winterborn.Tools.Vee.Evaluation;

/// <inheritdoc cref="IUnitsToCompare"/>
internal class UnitsToCompare(
    IReadOnlyList<IPackageableUnit> older,
    IReadOnlyList<IPackageableUnit> newer) : IUnitsToCompare
{
    public IReadOnlyList<IPackageableUnit> Older { get; } = older;

    public IReadOnlyList<IPackageableUnit> Newer { get; } = newer;
}
