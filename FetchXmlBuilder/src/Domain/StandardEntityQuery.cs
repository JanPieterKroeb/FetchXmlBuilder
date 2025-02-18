namespace FetchXmlBuilder.Domain;

public class StandardEntityQuery : EntityQuery
{
    // ReSharper disable once MemberCanBeProtected.Global
    public StandardEntityQuery(string entityName)
    {
        // TODO: All-attributes optional
        OpeningTag = $"<entity name=\"{entityName}\"><all-attributes />";
        ClosingTag = "</entity>";
    }
}