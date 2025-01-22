using System;
using System.Linq.Expressions;
using FetchXmlBuilder.Domain.Enums;
using FetchXmlBuilder.Helper;
using FetchXmlBuilder.Interfaces;

namespace FetchXmlBuilder.Domain
{
    public class FetchXmlQueryCollection<T> : IFetchXmlQueryCollection<T>, IXmlBlock
    {
        private readonly ConditionExpressionVisitor<T> _conditionExpressionVisitor = new ConditionExpressionVisitor<T>();
        private readonly FetchXmlBuilder _queryBuilder;
        
        public FetchXmlQueryCollection(string entityName)
        {
            _queryBuilder = new FetchXmlBuilder(entityName);
        }
        
        public IFetchXmlQueryCollection<T> Filter(Expression<Func<T, bool>> linqExpression, FilterType filterType = FilterType.And)
        {
            _queryBuilder.AddCondition(_conditionExpressionVisitor.GetConditionFromExpression(linqExpression));
            return this;
        }

        public IFetchXmlQueryCollection<T> LinkEntity(Expression<Func<T, object>> expandExpression)
        {
            throw new NotImplementedException();
        }

        public string ToFetchXmlString()
        {
            return _queryBuilder.ToFetchXml();
        }
    }
}