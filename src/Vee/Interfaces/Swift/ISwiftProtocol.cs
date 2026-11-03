namespace Winterborn.Tools.Vee.Interfaces.Swift;

public interface ISwiftProtocol : ISwiftType
{
    public IReadOnlyList<string> AssociatedTypes { get; }
}
