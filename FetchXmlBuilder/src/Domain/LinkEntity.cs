namespace FetchXmlBuilder.Domain.EntityProperties;

internal class LinkEntity : StandardEntityQuery
{
    internal LinkEntity(string entityName, string from, string to, string alias) : base(entityName)
    {
        // TODO: Remove hard-coded All-Attributes
        OpeningTag = $"<link-entity name=\"{entityName}\" from=\"{from}\" to=\"{to}\" link-type=\"outer\" alias=\"{alias}\"><all-attributes />";
        ClosingTag = "</link-entity>";
    }
}