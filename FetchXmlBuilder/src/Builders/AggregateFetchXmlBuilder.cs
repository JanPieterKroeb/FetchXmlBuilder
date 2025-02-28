using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Domain;
using FetchXmlBuilder.Domain.EntityProperties.Attributes;
using FetchXmlBuilder.Domain.Enums;
using FetchXmlBuilder.Interfaces;

namespace FetchXmlBuilder.Builders;

public class AggregateFetchXmlBuilder<T, TEntityQuery>
    : EntityFetchXmlBuilder<T, TEntityQuery>, IFetchXmlAggregateMethods<T> where TEntityQuery : EntityQuery
{
    public AggregateFetchXmlBuilder(string entityName, bool isDistinct) : base(entityName)
    {
        QueryStringBuilder = new FetchXmlAggregateStringBuilder(entityName, isDistinct) as IFetchXmlStringBuilder
                             ?? throw new InvalidOperationException();
    }
    
    /// <inheritdoc/>
    public IFetchXmlAggregateMethods<T> Aggregate(Expression<Func<T, object>> field, AggregateOperation operation, string alias, bool isDistinct)
    {
        switch (field.Body)
        {
            case UnaryExpression { Operand: MemberExpression mex }:
                QueryStringBuilder.AddAttribute(new AggregateAttribute(mex.Member.Name, alias, new AggregateFields(operation, isDistinct)));
                return this;
            case MemberExpression mex:
                QueryStringBuilder.AddAttribute(new AggregateAttribute(mex.Member.Name, alias, new AggregateFields(operation, isDistinct)));
                return this;
            default:
                throw new InvalidOperationException("Could not get member");
        }
    }
}