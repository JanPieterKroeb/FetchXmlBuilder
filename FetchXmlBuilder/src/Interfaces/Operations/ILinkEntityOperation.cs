using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Tasks.Link;

namespace FetchXmlBuilder.Interfaces.Operations;

public interface ILinkEntityOperation<out TXmlCondition, T>
{
    TXmlCondition LinkEntity<TLinkEntity>(
        Expression<Func<ILinkEntityResource<T>, object>> expandExpressionString,
        Expression<Func<TLinkEntity, object>> fromExpression,
        Expression<Func<T, object>> toExpressionString);
}