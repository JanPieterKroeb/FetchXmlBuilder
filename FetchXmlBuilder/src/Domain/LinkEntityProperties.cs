namespace FetchXmlBuilder.Domain
{
    internal class LinkEntityProperties
    {
        internal readonly string EntityName;
        internal readonly string? Alias;
        
        internal LinkEntityProperties(string entityName, string? alias = null)
        {
            EntityName = entityName;
            Alias = alias;
        }
    }
}