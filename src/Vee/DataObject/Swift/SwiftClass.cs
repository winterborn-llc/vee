using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.DataObject.Swift;

[XmlType("Class")]
public class SwiftClass : SwiftType, ISwiftClass
{
    public override string Kind => SwiftTypeKinds.Class;
}
