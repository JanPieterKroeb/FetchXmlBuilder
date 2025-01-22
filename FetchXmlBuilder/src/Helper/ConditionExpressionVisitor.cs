using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FetchXmlBuilder.Domain;
using FetchXmlBuilder.Domain.Enums;

namespace FetchXmlBuilder.Helper
{
    public class ConditionExpressionVisitor<T>
    {
        public IEnumerable<Condition> GetConditionsFromLambdaExpression(LambdaExpression lambdaExpression)
        {
            var conditions = new List<Condition>();
            if (lambdaExpression.NodeType == ExpressionType.And)
            {
                // conditions.AddRange(GetConditionsFromLambdaExpression(lambdaExpression.Body.Reduce())
            }
        
            throw new NotImplementedException();
        }

        public IEnumerable<Condition> GetConditionsFromBinaryExpression()
        {
            var conditions = new List<Condition>();
            if (lambdaExpression.NodeType == ExpressionType.And)
            {
                conditions.AddRange(GetConditionsFromBinaryExpression(lambdaExpression.Body.Reduce())
            }
            
            throw new NotImplementedException();
            BinaryExpression binaryExpression => GetConditionsFromBinaryExpression(binaryExpression),

        }
        
        public Condition GetConditionFromExpression(Expression expression)
        {
            return expression switch
            {
                MethodCallExpression methodCallExpression => GetConditionFromMethodCallExpression(methodCallExpression),
                LambdaExpression lambdaExpression => GetConditionFromLambdaExpression(lambdaExpression),
                BinaryExpression binaryExpression => GetConditionFromNonLogicalBinaryExpression(binaryExpression),
                _ => throw new InvalidOperationException()
            };
        }

        private Condition GetConditionFromLambdaExpression(LambdaExpression lambdaExpression) =>
            GetConditionFromExpression(lambdaExpression.Body);

        private static Condition GetConditionFromNonLogicalBinaryExpression(BinaryExpression binaryExpression)
        {
            MemberExpression memberExpression;
            // Validate if attribute is a member of type T
            if (binaryExpression.Left is MemberExpression mLeft && GetParentType(mLeft) == typeof(T))
            {
                memberExpression = mLeft;
            }
            else if (binaryExpression.Right is MemberExpression mRight && GetParentType(mRight) == typeof(T))
            {
                memberExpression = mRight;
            }
            else
            {
                throw new NotSupportedException($"A property of type '{typeof(T)}' needs to be in the Linq Expression!");
            }

            var value = GetValueFromExpression(memberExpression);
            var @operator = binaryExpression.NodeType switch
            {
                ExpressionType.Equal => value != null ? "eq" : "null",
                ExpressionType.NotEqual => value != null ? "ne" : "not-null",
                _ => throw new NotImplementedException("Unsupported binary expression!")
            };
                
            return new Condition(memberExpression.Member.Name, @operator, value ?? "");
        }
        
        private static Condition GetConditionFromMethodCallExpression(MethodCallExpression methodExpression)
        {
            if (methodExpression.Object is MemberExpression expression && GetParentType(expression) == typeof(T))
            {
                var attribute = expression.Member.Name;
                return methodExpression.Method.Name switch
                {
                    SupportedLinqMethods.StartsWith => new Condition(
                        attribute,
                        "like",
                        $"{GetValueFromExpression(methodExpression.Arguments[0])}%"),
                    SupportedLinqMethods.EndsWith => new Condition(
                        attribute,
                        "like",
                        $"%{GetValueFromExpression(methodExpression.Arguments[0])}"),
                    SupportedLinqMethods.Contains => new Condition(
                        attribute,
                        "like",
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
            _ => GetValue(expression.Member)?.ToString(),
        };

        private static object? GetValue(MemberInfo memberInfo, object? obj = default) => memberInfo switch
        {
            FieldInfo fieldInfo => fieldInfo.GetValue(obj),
            PropertyInfo propertyInfo => propertyInfo.GetValue(obj, default),
            _ => default
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
}