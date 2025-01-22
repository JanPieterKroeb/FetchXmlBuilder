using System;
using System.Linq.Expressions;

namespace FetchXmlBuilder.Interfaces
{
    public interface IXmlCondition<TXmlCondition, T>
    {
        TXmlCondition LinkEntity(Expression<Func<T, object>> expandExpression);
    }
}