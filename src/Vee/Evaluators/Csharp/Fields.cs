using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.Evaluators.Csharp;

internal static class Fields
{
    internal static ICsharpField? Find(ICsharpType type, string name)
    {
        foreach (var field in type.Fields)
        {
            if (field.Name != name)
            {
                continue;
            }

            return field;
        }

        return null;
    }
}
