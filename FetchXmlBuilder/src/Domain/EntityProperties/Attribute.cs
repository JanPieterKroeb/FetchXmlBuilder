using System.ComponentModel;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Domain.EntityProperties;

internal class Attribute(string name, string? alias, AggregateFields? aggregateFields)
{
    private string Name { get; } = name;
    private string? Alias { get; } = alias;
    private AggregateFields? AggregateFields { get; } = aggregateFields;

    public override string ToString()
    {
        if (AggregateFields != null)
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
        else
        {
            return $"<attribute name=\"{Name}\" alias=\"{Alias}\"/>";
        }
    }
        
}