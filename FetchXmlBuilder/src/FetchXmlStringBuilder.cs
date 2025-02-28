using System;
using System.Text;
using FetchXmlBuilder.Domain;
using FetchXmlBuilder.Domain.EntityProperties;
using FetchXmlBuilder.Domain.EntityProperties.Attributes;
using FetchXmlBuilder.Interfaces;

namespace FetchXmlBuilder;

internal class FetchXmlStringBuilder<TEntity> : IFetchXmlStringBuilder where TEntity : EntityQuery
{
    protected StringBuilder _builder;
    private readonly TEntity _mainEntity;
    protected string OpeningTag;
    private const string ClosingTag = "</fetch>";

    public FetchXmlStringBuilder(string entityName)
    {
        OpeningTag = "<fetch returntotalrecordcount=\"true\">";
        _builder = new StringBuilder(OpeningTag);
        _mainEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), [entityName]);
    }
        
    public FetchXmlStringBuilder(string entityName, int count, int page)
    {
        OpeningTag = $"<fetch returntotalrecordcount=\"true\" count=\"{count}\" page=\"{page}\">";
        _builder = new StringBuilder(OpeningTag);
        _mainEntity = (TEntity)Activator.CreateInstance(typeof(TEntity), [entityName]);
    }
        
    public string ToFetchXml()
    {
        _builder.Append(_mainEntity);
        _builder.Append(ClosingTag);
        var fetchXml = _builder.ToString();
        _builder.Clear();
        _builder.Append(OpeningTag);
        return fetchXml;
    }

    public void AddCondition(Condition condition)
    {
        _mainEntity.ConditionsAnd.Add(condition);
    }

    public void AddLinkedEntity(LinkEntity entity)
    {
        _mainEntity.LinkEntities.Add(entity);
    }

    public void AddOrder(Order order)
    {
        _mainEntity.Orders.Add(order);
    }

    public void AddAttribute(BaseAttribute baseAttribute)
    {
        _mainEntity.Attributes.Add(baseAttribute);
    }
}