using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Tasks.Link;

namespace FetchXmlBuilder.Interfaces
{
    public interface IXmlCondition<TXmlCondition, T>
    {
        TXmlCondition LinkEntity<TLinkEntity>(
            Expression<Func<ILinkEntityResource<T>, object>> expandExpressionString,
            Expression<Func<TLinkEntity, object>> fromExpression,
            Expression<Func<T, object>> toExpressionString);
    }
}