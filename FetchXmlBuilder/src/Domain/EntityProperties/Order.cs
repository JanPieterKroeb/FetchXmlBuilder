namespace FetchXmlBuilder.Domain.EntityProperties;

internal class Order
{
    public readonly string Attribute;
    public readonly bool IsDescending;

    internal Order(string attribute)
    {
        Attribute = attribute;
        IsDescending = true;
    }

    internal Order(string attribute, bool isDescending)
    {
        Attribute = attribute;
        IsDescending = isDescending;
    }
}