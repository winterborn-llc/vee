namespace Winterborn.Tools.Vee.Interfaces.Swift;

public interface ISwiftGenericParameter
{
    public string Name { get; }

    /// <summary>Constraints sorted and comma-joined, so S12/S13 compare them as sets.</summary>
    public string Constraints { get; }
}
