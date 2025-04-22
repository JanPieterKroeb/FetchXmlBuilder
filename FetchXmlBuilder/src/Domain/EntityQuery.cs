using System.Collections.Generic;
using FetchXmlBuilder.Domain.EntityProperties;
using FetchXmlBuilder.Domain.EntityProperties.Attributes;

namespace FetchXmlBuilder.Domain;

public abstract class EntityQuery
{
    internal readonly List<BaseAttribute> Attributes = [];
    internal readonly List<Condition> ConditionsAnd = [];
    internal readonly List<Condition> ConditionsOr = [];
    internal readonly List<Order> Orders = [];
    internal readonly List<LinkEntity> LinkEntities = [];
        
    protected string OpeningTag;
    protected string ClosingTag;
    
    /// <summary>
    /// Writes the entity / link-entity as a FetchXML block.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var xmlString = OpeningTag;
            
        // Build filter
        if (ConditionsAnd.Count > 0)
        {
            xmlString += "<filter>";
            foreach (var condition in ConditionsAnd)
            {
                xmlString += $"<condition attribute=\"{condition.Attribute}\" operator=\"{condition.Operator}\" value=\"{condition.Value ?? ""}\" />";
            }
            
            xmlString += "</filter>";
        }
        foreach (var attribute in Attributes)
        {
            xmlString += attribute.ToString();
        }
            
        // Build ordering
        foreach (var order in Orders)
        {
            xmlString += $"<order attribute=\"{order.Attribute}\" descending=\"{order.IsDescending.ToString().ToLower()}\" />";
        }
        // Build linked entities
        foreach (var linkEntity in LinkEntities)
        {
            xmlString += linkEntity.ToString();
        }

        xmlString += ClosingTag;
        return xmlString;
    }
}