using System;
using System.Linq.Expressions;
using FetchXmlBuilder.ParameterOptions;
using FetchXmlBuilder.Tasks.Link;

namespace FetchXmlBuilder.Interfaces.Operations;

public interface IOperations<out TXmlCondition, T>
{
    TXmlCondition CountChildren(Expression<Func<T, object>> primaryKeyExpression, string variableName);
    
    TXmlCondition FilterByHierarchy(Expression<Func<T, object>> primaryKeyExpression, Guid primaryKeyValue,
        HierarchyFilterOptions option);
    
    TXmlCondition Filter(Expression<Func<T, bool>> filter);
    
    TXmlCondition LinkEntity<TLinkEntity>(
        Expression<Func<ILinkEntityResource<T>, object>> expandExpressionString,
        Expression<Func<TLinkEntity, object>> fromExpression,
        Expression<Func<T, object>> toExpressionString);
    
    TXmlCondition OrderBy(Expression<Func<T, object>> fieldExpression, bool isDescending = false);
}