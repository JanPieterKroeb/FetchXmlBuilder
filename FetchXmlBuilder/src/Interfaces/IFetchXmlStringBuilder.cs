using FetchXmlBuilder.Domain;
using FetchXmlBuilder.Domain.EntityProperties;
using FetchXmlBuilder.Domain.EntityProperties.Attributes;

namespace FetchXmlBuilder.Interfaces;

internal interface IFetchXmlStringBuilder
{
    public string ToFetchXml();
    public void AddCondition(Condition condition);

    public void AddLinkedEntity(LinkEntity entity);

    public void AddOrder(Order order);

    public void AddAttribute(BaseAttribute baseAttribute);
}