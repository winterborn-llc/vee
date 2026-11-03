using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[DebuggerDisplay("{Type} {Name}")]
[XmlType("Field")]
public class CsharpField : ICsharpField
{
    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("type")]
    public string Type { get; set; } = string.Empty;

    [XmlAttribute("static")]
    public bool IsStatic { get; set; }

    [XmlAttribute("readonly")]
    public bool IsReadOnly { get; set; }

    [XmlAttribute("const")]
    public bool IsConstant { get; set; }
}
