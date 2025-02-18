using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Domain.EntityProperties;

internal class AggregateFields(AggregateOperation operation, bool isDistinct)
{
    public AggregateOperation Operation { get; } = operation;
    public bool IsDistinct { get; } = isDistinct;
}