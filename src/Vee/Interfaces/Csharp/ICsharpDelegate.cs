namespace Winterborn.Tools.Vee.Interfaces.Csharp;

public interface ICsharpDelegate : ICsharpType
{
    public string ReturnType { get; }

    public IReadOnlyList<ICsharpMethodParameter> Parameters { get; }
}
