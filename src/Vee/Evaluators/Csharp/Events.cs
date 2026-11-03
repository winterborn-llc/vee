using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

internal static class Events
{
    internal static ICsharpEvent? Find(ICsharpType type, string name)
    {
        foreach (var candidate in type.Events)
        {
            if (candidate.Name != name)
            {
                continue;
            }

            return candidate;
        }

        return null;
    }
}
