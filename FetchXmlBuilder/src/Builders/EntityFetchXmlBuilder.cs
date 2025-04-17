using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Domain;
using FetchXmlBuilder.Domain.EntityProperties;
using FetchXmlBuilder.Domain.EntityProperties.Attributes;
using FetchXmlBuilder.Domain.Enums;
using FetchXmlBuilder.Helper;
using FetchXmlBuilder.Interfaces;
using FetchXmlBuilder.ParameterOptions;
using FetchXmlBuilder.Tasks.Link;

namespace FetchXmlBuilder.Builders;

public class EntityFetchXmlBuilder<T, TEntityQuery>(string entityName) : IFetchXmlQueryMethods<T> where TEntityQuery : EntityQuery
{
    private protected IFetchXmlStringBuilder QueryStringBuilder = new FetchXmlStringBuilder<TEntityQuery>(entityName);
    private readonly ConditionExpressionVisitor<T> _conditionExpressionVisitor = new ConditionExpressionVisitor<T>();

    public IFetchXmlQueryMethods<T> Filter(Expression<Func<T, bool>> linqExpression)
    {
        foreach (var condition in _conditionExpressionVisitor.GetConditionsFromLambdaExpression(linqExpression))
        {
            QueryStringBuilder.AddCondition(condition);
        }
        return this;
    }

    public IFetchXmlQueryMethods<T> Filter(Expression<Func<T, object>> fieldExpression, ExpressionType expressionType, object value)
    {
        QueryStringBuilder.AddCondition(new Condition(ExtractPropertyName(fieldExpression), expressionType, value.ToString()));
        return this;
    }

    public IFetchXmlQueryMethods<T> LinkEntity<TLinkEntity>(
        Expression<Func<ILinkEntityResource<T>, object>> expandExpressionString,
        Expression<Func<TLinkEntity, object>> fromExpression,
        Expression<Func<T, object>> toExpression)
    {
        // TODO: Add conditions to linked entities
        var linkEntityName = GetLinkEntityProperties(expandExpressionString);
        var fromField = ExtractPropertyName(fromExpression);
        var toField = ExtractPropertyName(toExpression);
            
        QueryStringBuilder.AddLinkedEntity(new LinkEntity(
            linkEntityName.EntityName,
            fromField,
            toField,
            linkEntityName.Alias ?? linkEntityName.EntityName));
        return this;
    }

    public IFetchXmlQueryMethods<T> OrderBy(Expression<Func<T, object>> fieldExpression, bool isDescending = false)
    {
        var orderPropertyName = ExtractPropertyName(fieldExpression);
        QueryStringBuilder.AddOrder(new Order(orderPropertyName, isDescending));
        return this;
    }

    public IFetchXmlQueryMethods<T> FilterByHierarchy(Expression<Func<T, object>> primaryKeyExpression,
        Guid primaryKeyValue,
        HierarchyFilterOptions option)
    {
        var propertyName = ExtractPropertyName(primaryKeyExpression);

        var operation = option switch
        {
            HierarchyFilterOptions.Under => HierarchyFilterOperation.Under,
            HierarchyFilterOptions.UnderOrEqual => HierarchyFilterOperation.UnderOrEqual,
            HierarchyFilterOptions.Above => HierarchyFilterOperation.Above,
            HierarchyFilterOptions.AboveOrEqual => HierarchyFilterOperation.AboveOrEqual,
            HierarchyFilterOptions.NotUnder => HierarchyFilterOperation.NotUnder,
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };
        QueryStringBuilder.AddCondition(new Condition(propertyName, operation, primaryKeyValue.ToString()));
        return this;
    }
    
    public IFetchXmlQueryMethods<T> CountChildren(Expression<Func<T, object>> primaryKeyExpression, string variableName)
    {
        var propertyName = ExtractPropertyName(primaryKeyExpression);

        QueryStringBuilder.AddAttribute(new AggregateRowAttribute(propertyName, variableName));
        return this;
    }
        
    public string ToFetchXmlString()
    {
        return QueryStringBuilder.ToFetchXml();
    }

    private static LinkEntityProperties GetLinkEntityProperties(Expression<Func<ILinkEntityResource<T>, object>> expandExpression)
    {
        if (expandExpression.Body is MethodCallExpression methodCallExpression)
        {
            // Check if the method call is 'For' and get its argument
            if (methodCallExpression.Method.Name == "For")
            {
                var argument = methodCallExpression.Arguments[0];

                // Handle a lambda expression within the 'For' call
                if (argument is UnaryExpression { Operand: LambdaExpression lambdaExpression })
                    // Get the property access (MemberExpression) inside the lambda
                {
                    // TODO: Fix ugly block
                    switch (lambdaExpression.Body)
                    {
                        case MemberExpression memberExpression when
                            methodCallExpression.Arguments is [_, ConstantExpression constantExpression]:
                            return new LinkEntityProperties(memberExpression.Member.Name,
                                constantExpression.Value?.ToString());
                        case MemberExpression memberExpression:
                            return new LinkEntityProperties(memberExpression.Member.Name);
                        case UnaryExpression { Operand: MemberExpression nullableMemberExpression } when
                            methodCallExpression.Arguments is [_, ConstantExpression constantExpression]:
                            return new LinkEntityProperties(nullableMemberExpression.Member.Name,
                                constantExpression.Value?.ToString());
                        case UnaryExpression { Operand: MemberExpression nullableMemberExpression }:
                            return new LinkEntityProperties(nullableMemberExpression.Member.Name);
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Please use the For method!");
            }
        }

        throw new InvalidOperationException();
    }

    private static string ExtractPropertyName<TObject>(Expression<Func<TObject, object>> expression)
    {
        // Unwrap the expression body
        var body = expression.Body;

        // Handle cases where the expression body is a UnaryExpression (e.g., for value types boxed as object)
        if (body is UnaryExpression unaryExpression)
        {
            body = unaryExpression.Operand;
        }
            
        // Ensure the body is a MemberExpression (e.g., accessing a property or field)
        if (body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name; // Return the name of the property/field
        }

        throw new InvalidOperationException("Expression does not represent a property or field access.");
    }
}