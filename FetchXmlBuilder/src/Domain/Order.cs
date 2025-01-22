namespace FetchXmlBuilder.Domain
{
    public class Order
    {
        public readonly string Attribute;
        public readonly bool IsDescending;

        public Order(string attribute)
        {
            Attribute = attribute;
            IsDescending = true;
        }

        public Order(string attribute, bool isDescending)
        {
            Attribute = attribute;
            IsDescending = isDescending;
        }
    }
}