using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[DebuggerDisplay("{Name} : {Constraints}")]
[XmlType("GenericParameter")]
public class CsharpGenericParameter : ICsharpGenericParameter
{
    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("constraints")]
    public string Constraints { get; set; } = string.Empty;
}
