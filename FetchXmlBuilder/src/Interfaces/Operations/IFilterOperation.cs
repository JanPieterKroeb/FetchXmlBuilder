using System;
using System.Linq.Expressions;
using FetchXmlBuilder.ParameterOptions;

namespace FetchXmlBuilder.Interfaces.Operations;

public interface IFilterOperation<out TXmlCondition, T> : ILinkEntityOperation<TXmlCondition, T>
{
    TXmlCondition Filter(Expression<Func<T, bool>> filter);

    TXmlCondition FilterByHierarchy(Expression<Func<T, object>> primaryKeyExpression, Guid primaryKeyValue,
        HierarchyFilterOptions option);
}