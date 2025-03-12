namespace FetchXmlBuilder.Domain.EntityProperties;

internal class Order
{
    public readonly string Attribute;
    public readonly bool IsDescending;

    internal Order(string attribute)
    {
        Attribute = attribute;
        IsDescending = false;
    }

    internal Order(string attribute, bool isDescending)
    {
        Attribute = attribute;
        IsDescending = isDescending;
    }
}