using System.Collections.Generic;

namespace FetchXmlBuilder.Domain
{
    internal class Entity
    {
        public readonly List<Condition> ConditionsAnd = new List<Condition>();
        public readonly List<Condition> ConditionsOr = new List<Condition>();
        public readonly List<Order> Orders = new List<Order>();
        public readonly List<LinkEntity> LinkEntities = new List<LinkEntity>();
        protected string OpeningTag;
        protected string ClosingTag;

        public Entity(string entityName)
        {
            // TODO: All-attributes optional
            OpeningTag = $"<entity name=\"{entityName}\"><all-attributes />";
            ClosingTag = "</entity>";
        }

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
                    xmlString += $"<condition attribute=\"{condition.Attribute}\" operator=\"{condition.Operator}\" value=\"{condition.Value}\" />";
                }
            
                xmlString += "</filter>";
            }
            
            // Build ordering
            foreach (var order in Orders)
            {
                xmlString += $"<order attribute=\"{order.Attribute}\" descending=\"{order.IsDescending}\" />";
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
}