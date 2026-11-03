using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.DataObject.Swift;

[XmlType("Struct")]
public class SwiftStruct : SwiftType, ISwiftStruct
{
    public override string Kind => SwiftTypeKinds.Struct;
}
