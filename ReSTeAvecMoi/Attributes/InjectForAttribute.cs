namespace ReSTeAvecMoi.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited =false)]
public class InjectForAttribute(Type interfaceType) : Attribute
{
    public Type InterfaceType { get; } = interfaceType;
}
