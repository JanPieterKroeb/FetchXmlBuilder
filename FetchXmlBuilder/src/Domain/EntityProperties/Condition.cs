using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Domain.EntityProperties;

internal record Condition(string Attribute, string Operator, string? Value)
{
    internal Condition(string attribute, ExpressionType expressionType, string? value = null)
        : this(attribute, ExpressionTypeToOperation(expressionType, value), value)
    {}
    
    private static string ExpressionTypeToOperation(ExpressionType expressionType, string? value) => expressionType switch
    {
        ExpressionType.Equal => string.IsNullOrEmpty(value) ? XmlOperations.IsNull : XmlOperations.Equal,
        ExpressionType.NotEqual => string.IsNullOrEmpty(value) ?  XmlOperations.IsNotNull : XmlOperations.NotEqual,
        ExpressionType.LessThan => XmlOperations.LessThan,
        ExpressionType.LessThanOrEqual => XmlOperations.LessThanOrEqual,
        ExpressionType.GreaterThan => XmlOperations.GreaterThan,
        ExpressionType.GreaterThanOrEqual => XmlOperations.GreaterThanOrEqual,
        _ => throw new NotImplementedException("Unsupported binary expression!")
    };
}

