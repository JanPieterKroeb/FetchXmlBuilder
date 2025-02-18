using System;
using System.Linq.Expressions;

namespace FetchXmlBuilder.Interfaces.Operations;

public interface IFilterOperation<out TXmlCondition, T> : ILinkEntityOperation<TXmlCondition, T>
{
    TXmlCondition Filter(Expression<Func<T, bool>> filter);
}