using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[DebuggerDisplay("{Name} = {Value}")]
[XmlType("Member")]
public class CsharpEnumMember : ICsharpEnumMember
{
    [XmlAttribute("name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("value")]
    public string Value { get; set; } = string.Empty;
}
