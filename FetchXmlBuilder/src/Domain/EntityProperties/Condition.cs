namespace FetchXmlBuilder.Domain.EntityProperties;

internal class Condition(string attribute, string @operator, string value)
{
    public readonly string Attribute = attribute;
    public readonly string Operator = @operator;
    public readonly string Value = value;
}