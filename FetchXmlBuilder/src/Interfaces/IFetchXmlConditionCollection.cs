using System;
using System.Linq.Expressions;

namespace FetchXmlBuilder.Interfaces
{
    public interface IFetchXmlConditionCollection<TXmlCondition, T> : IXmlCondition<TXmlCondition, T>
    {
        TXmlCondition Filter(Expression<Func<T, bool>> filter);
    }
}