using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Interfaces.Operations;

public interface IAggregateOperation<out TXmlCondition, T> : IOperations<TXmlCondition, T>
{
    /// <summary>
    /// Creates attribute:<para>
    /// <attribute name="fieldSelection" alias="alias" aggregate="operation" distinct="isDistinct"/>
    /// </para>
    /// </summary>
    IFetchXmlAggregateMethods<T> Aggregate(Expression<Func<T, object>> field, 
        AggregateOperation operation, string alias, bool isDistinct);
}