using System.Text;
using FetchXmlBuilder.Domain;

namespace FetchXmlBuilder;

internal class FetchXmlAggregateStringBuilder : FetchXmlStringBuilder<AggregateEntityQuery>
{
    public FetchXmlAggregateStringBuilder(string entityName, bool isDistinct) : base(entityName)
    {
        OpeningTag = $"<fetch distinct=\"{isDistinct}\" aggregate=\"true\">";
        _builder = new StringBuilder(OpeningTag);
    }
}