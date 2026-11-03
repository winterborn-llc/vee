using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[XmlType("Interface")]
public class CsharpInterface : CsharpType, ICsharpInterface
{
    public override string Kind => CsharpTypeKinds.Interface;
}
