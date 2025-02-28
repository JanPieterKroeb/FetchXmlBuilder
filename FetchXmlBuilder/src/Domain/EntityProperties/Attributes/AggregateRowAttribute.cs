namespace FetchXmlBuilder.Domain.EntityProperties.Attributes;

internal class AggregateRowAttribute(string name, string? alias) : BaseAttribute(name, alias)
{
    public override string ToString() => $"<attribute name=\"{Name}\" alias=\"{Alias}\" rowaggregate=\"CountChildren\"/>";
}