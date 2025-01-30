using System.Text;
using FetchXmlBuilder.Domain;

namespace FetchXmlBuilder
{
    internal class FetchXmlStringBuilder
    {
        private readonly StringBuilder _builder;
        private readonly Entity _mainEntity;

        public FetchXmlStringBuilder(string entityName)
        {
           _builder = new StringBuilder("<fetch returntotalrecordcount=\"true\">");
           _mainEntity = new Entity(entityName);
        }
        
        public FetchXmlStringBuilder(string entityName, int count, int page)
        {
            _builder = new StringBuilder($"<fetch returntotalrecordcount=\"true\" count=\"{count}\" page=\"{page}\">");
            _mainEntity = new Entity(entityName);
        }
        
        public string ToFetchXml()
        {
            _builder.Append(_mainEntity);
            _builder.Append("</fetch>");
            var fetchXml = _builder.ToString();
            _builder.Clear();
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
    }
}