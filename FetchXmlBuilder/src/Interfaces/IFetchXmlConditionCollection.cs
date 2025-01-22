using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Interfaces
{
    public interface IFetchXmlConditionCollection<TXmlCondition, T> : IXmlCondition<TXmlCondition, T>
    {
        TXmlCondition Filter(Expression<Func<T, bool>> filter, FilterType filterType = FilterType.And);
    }
}