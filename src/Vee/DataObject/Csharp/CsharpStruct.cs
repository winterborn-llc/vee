using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[XmlType("Struct")]
public class CsharpStruct : CsharpType, ICsharpStruct
{
    public override string Kind => CsharpTypeKinds.Struct;
}
