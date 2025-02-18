using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FetchXmlBuilder.Domain.EntityProperties;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Helper;

internal class ConditionExpressionVisitor<T>
{
    public IEnumerable<Condition> GetConditionsFromLambdaExpression(LambdaExpression lambdaExpression) =>
        GetConditionsFromAllExpressions(lambdaExpression.Body);

    private IEnumerable<Condition> GetConditionsFromAllExpressions(Expression expression, bool negate = false)
    {
        switch (expression)
        {
            case BinaryExpression actualBinaryExpression:
            {
                var conditions = new List<Condition>();
                switch (expression.NodeType)
                {
                    case ExpressionType.AndAlso:
                        conditions.AddRange(GetConditionsFromAllExpressions(actualBinaryExpression.Left, negate));
                        conditions.AddRange(GetConditionsFromAllExpressions(actualBinaryExpression.Right, negate));
                        break;
                    case ExpressionType.OrElse:
                        throw new NotSupportedException("Ors are not supported yet! They will be!");
                    default:
                        conditions.Add(GetConditionFromExpression(actualBinaryExpression, negate));
                        break;
                }
                return conditions;
            }
            case UnaryExpression { NodeType: ExpressionType.Not } unaryExpression:
                GetConditionsFromAllExpressions(unaryExpression.Operand, !negate);
                break;
        }

        return new List<Condition> { GetConditionFromExpression(expression) };
    }

    private Condition GetConditionFromExpression(Expression expression, bool negate = false)
    {
        return expression switch
        {
            MethodCallExpression methodCallExpression => GetConditionFromMethodCallExpression(methodCallExpression, negate),
            LambdaExpression lambdaExpression => GetConditionFromLambdaExpression(lambdaExpression),
            BinaryExpression binaryExpression => GetConditionFromNonLogicalBinaryExpression(binaryExpression, negate),
            MemberExpression memberExpression => GetConditionFromBooleanMemberField(memberExpression, negate),
            UnaryExpression unaryExpression => GetConditionFromExpression(unaryExpression.Operand, !negate),
            _ => throw new InvalidOperationException()
        };
    }

    private Condition GetConditionFromLambdaExpression(LambdaExpression lambdaExpression) =>
        GetConditionFromExpression(lambdaExpression.Body);

    private static Condition GetConditionFromBooleanMemberField(MemberExpression memberExpression, bool negate)
    {
        if (memberExpression.Member is PropertyInfo propertyInfo && propertyInfo.PropertyType == typeof(bool))
        {
            return new Condition(
                memberExpression.Member.Name,
                XmlOperations.Equal,
                negate ? "0" : "1");
        }
        throw new NotImplementedException();
    }
        
    private static Condition GetConditionFromNonLogicalBinaryExpression(BinaryExpression binaryExpression, bool negate)
    {
        MemberExpression memberExpression;
        Expression expressionWithValue;
        // Validate if attribute is a member of type T
        if (binaryExpression.Left is MemberExpression mLeft && GetParentType(mLeft) == typeof(T))
        {
            memberExpression = mLeft;
            expressionWithValue = binaryExpression.Right;
        }
        else if (binaryExpression.Right is MemberExpression mRight && GetParentType(mRight) == typeof(T))
        {
            memberExpression = mRight;
            expressionWithValue = binaryExpression.Left;
        }
        else
        {
            throw new NotSupportedException($"A property of type '{typeof(T)}' needs to be in the Linq Expression!");
        }

        var value = GetValueFromExpression(expressionWithValue);
        var @operator = binaryExpression.NodeType switch
        {
            ExpressionType.Equal => value != null ? XmlOperations.Equal : XmlOperations.IsNull,
            ExpressionType.NotEqual => value != null ? XmlOperations.NotEqual : XmlOperations.IsNotNull,
            _ => throw new NotImplementedException("Unsupported binary expression!")
        };
        if (negate)
        {
            @operator = @operator switch
            {
                XmlOperations.Equal => XmlOperations.NotEqual,
                XmlOperations.NotEqual => XmlOperations.Equal,
                XmlOperations.IsNull => XmlOperations.IsNotNull,
                XmlOperations.IsNotNull => XmlOperations.IsNull
            };
        }

                
        return new Condition(memberExpression.Member.Name, @operator, value ?? "");
    }
        
    private static Condition GetConditionFromMethodCallExpression(MethodCallExpression methodExpression, bool negate)
    {
        if (methodExpression.Object is MemberExpression expression && GetParentType(expression) == typeof(T))
        {
            var attribute = expression.Member.Name;
            return methodExpression.Method.Name switch
            {
                SupportedLinqMethods.StartsWith => new Condition(
                    attribute,
                    negate ? XmlOperations.NotLike : XmlOperations.Like,
                    $"{GetValueFromExpression(methodExpression.Arguments[0])}%"),
                SupportedLinqMethods.EndsWith => new Condition(
                    attribute,
                    negate ? XmlOperations.NotLike : XmlOperations.Like,
                    $"%{GetValueFromExpression(methodExpression.Arguments[0])}"),
                SupportedLinqMethods.Contains => new Condition(
                    attribute,
                    negate ? XmlOperations.NotLike : XmlOperations.Like,
                    $"%{GetValueFromExpression(methodExpression.Arguments[0])}%"),
                _ => throw new NotImplementedException("Unsupported method!")
            };
        }
        throw new InvalidOperationException();
    }

    private static string? GetValueFromExpression(Expression expression) => expression switch
    {
        MemberExpression memberExpression => GetValueOfMemberExpression(memberExpression),
        ConstantExpression constantExpression => GetValueFromConstantExpression(constantExpression),
        _ => throw new InvalidOperationException()
    };
        
    private static string GetValueFromConstantExpression(ConstantExpression constantExpression) =>
        constantExpression.Value.ToString();
        
    private static string? GetValueOfMemberExpression(MemberExpression expression) => expression.Expression switch
    {
        ConstantExpression constantExpression => GetValue(expression.Member, constantExpression.Value)?.ToString(),
        MemberExpression memberExpression => GetValue(expression.Member, GetValueOfMemberExpression(memberExpression))?.ToString(),
        _ => GetValue(expression.Member, null)?.ToString(),
    };

    private static object GetValue(MemberInfo memberInfo, object obj) => memberInfo switch
    {
        FieldInfo fieldInfo => fieldInfo.GetValue(obj),
        PropertyInfo propertyInfo => propertyInfo.GetValue(obj, default),
        _ => throw new InvalidOperationException($"Cannot get the value for object {obj}")
    };
        
    private static Type GetParentType(MemberExpression memberExpression)
    {
        return memberExpression.Expression switch
        {
            // The `Expression` property represents the parent object
            ParameterExpression parameterExpression => parameterExpression.Type,
            MemberExpression innerMemberExpression => GetParentType(innerMemberExpression),
            _ => throw new InvalidOperationException("Unable to determine the parent type.")
        };
    }
}