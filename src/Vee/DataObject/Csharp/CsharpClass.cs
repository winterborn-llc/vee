using System.Xml.Serialization;
using Winterborn.Tools.Vee.Interfaces.Csharp;

namespace Winterborn.Tools.Vee.DataObject.Csharp;

[XmlType("Class")]
public class CsharpClass : CsharpType, ICsharpClass
{
    public override string Kind => CsharpTypeKinds.Class;
}
