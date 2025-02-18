using FetchXmlBuilder.Builders;
using FetchXmlBuilder.Domain;

namespace FetchXmlBuilder;

public class QueryBuilder
{
    public EntityFetchXmlBuilder<T, StandardEntityQuery> For<T>(string entityName) =>
        new EntityFetchXmlBuilder<T, StandardEntityQuery>(entityName);
    
    public AggregateFetchXmlBuilder<T, AggregateEntityQuery> AggregateFor<T>(string entityName, bool isDistinct) =>
        new AggregateFetchXmlBuilder<T, AggregateEntityQuery>(entityName, isDistinct);
}