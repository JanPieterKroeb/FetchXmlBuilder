using System;
using System.Linq.Expressions;

namespace FetchXmlBuilder.Interfaces.Operations;

public interface IAttributeOperation<out TXmlCondition, T> : IFilterOperation<TXmlCondition, T>
{
    TXmlCondition CountChildren(Expression<Func<T, object>> primaryKeyExpression, string variableName);
}