using System.ComponentModel;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Domain.EntityProperties.Attributes;

internal class AggregateAttribute(string name, string? alias, AggregateFields aggregateFields) : BaseAttribute(name, alias)
{
    private AggregateFields AggregateFields { get; } = aggregateFields;
    public override string ToString()
    {
        var aggregateOperation = AggregateFields.Operation switch
        {
            AggregateOperation.Average => "avg",
            AggregateOperation.Count => "count",
            AggregateOperation.CountColumn => "countcolumn",
            AggregateOperation.Max => "max",
            AggregateOperation.Min => "min",
            AggregateOperation.Sum => "sum",
            _ => throw new InvalidEnumArgumentException()
        };
        return $"<attribute name=\"{Name}\" alias=\"{Alias}\" aggregate=\"{aggregateOperation}\" distinct=\"{AggregateFields.IsDistinct}\"/>";
    }
}