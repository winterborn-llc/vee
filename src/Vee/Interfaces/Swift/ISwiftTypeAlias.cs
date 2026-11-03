namespace Winterborn.Tools.Vee.Interfaces.Swift;

public interface ISwiftTypeAlias : ISwiftDeclaration
{
    public string UnderlyingType { get; }
}
