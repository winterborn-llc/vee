using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Swift;

namespace Winterborn.Tools.Vee.DataObject.Swift;

[XmlType("Actor")]
public class SwiftActor : SwiftType, ISwiftActor
{
    public override string Kind => SwiftTypeKinds.Actor;
}
