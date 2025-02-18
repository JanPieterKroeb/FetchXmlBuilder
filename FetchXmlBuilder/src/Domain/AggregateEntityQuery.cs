namespace FetchXmlBuilder.Domain;

public class AggregateEntityQuery : EntityQuery
{
    public AggregateEntityQuery(string entityName)
    {
        OpeningTag = $"<entity name=\"{entityName}\">";
        ClosingTag = "</entity>";
    }
}