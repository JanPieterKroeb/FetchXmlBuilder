namespace FetchXmlBuilder.Domain
{
    public class LinkEntity : Entity
    {
        public LinkEntity(string entityName, string entityIdField, string linkedEntityName, string alias) : base(entityName)
        {
            OpeningTag = $"<link-entity name=\"{entityName}\" from=\"{entityIdField}\" to=\"{linkedEntityName}\" link-type=\"outer\" alias=\"{alias}\">";
            ClosingTag = "</link-entity>";
        }
    }
}