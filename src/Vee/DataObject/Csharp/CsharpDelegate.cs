using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[XmlType("Delegate")]
public class CsharpDelegate : CsharpType, ICsharpDelegate
{
    public override string Kind => CsharpTypeKinds.Delegate;

    [XmlAttribute("returns")]
    public string ReturnType { get; set; } = string.Empty;

    [XmlArray("Parameters")]
    [XmlArrayItem("Parameter")]
    public List<CsharpMethodParameter> Parameters { get; set; } = [];

    IReadOnlyList<ICsharpMethodParameter> ICsharpDelegate.Parameters => this.Parameters;
}
