using System.Collections.Generic;

namespace FetchXmlBuilder.Domain
{
    public class Entity
    {
        public readonly List<Condition> Conditions = new List<Condition>();
        public readonly List<Order> Orders = new List<Order>();
        public readonly List<LinkEntity> LinkEntities = new List<LinkEntity>();
        protected string OpeningTag;
        protected string ClosingTag;

        public Entity(string entityName)
        {
            OpeningTag = $"<entity name=\"{entityName}\">";
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
            if (Conditions.Count > 0)
            {
                xmlString += "<filter>";
                foreach (var condition in Conditions)
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