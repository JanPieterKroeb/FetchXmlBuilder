namespace FetchXmlBuilder.Domain
{
    public class Condition
    {
        public readonly string Attribute;
        public readonly string Operator;
        public readonly string Value;

        public Condition(string attribute, string @operator, string value)
        {
            Attribute = attribute;
            Operator = @operator;
            Value = value;
        }
    }
}