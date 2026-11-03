namespace Winterborn.Tools.Vee.Interfaces.Csharp;

public interface ICsharpEvent
{
    public string Name { get; }

    public string HandlerType { get; }

    public bool IsStatic { get; }
}
