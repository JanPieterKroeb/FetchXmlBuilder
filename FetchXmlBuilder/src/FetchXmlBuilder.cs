using System.Text;
using FetchXmlBuilder.Domain;

namespace FetchXmlBuilder
{
    public class FetchXmlBuilder
    {
        private readonly StringBuilder _builder;
        private readonly Entity _mainEntity;

        public FetchXmlBuilder(string entityName)
        {
           _builder = new StringBuilder("<fetch returntotalrecordcount=\"true\">");
           _mainEntity = new Entity(entityName);
        }
        
        public FetchXmlBuilder(string entityName, int count, int page)
        {
            _builder = new StringBuilder($"<fetch returntotalrecordcount=\"true\" count=\"{count}\" page=\"{page}\">");
            _mainEntity = new Entity(entityName);
        }
        
        public string ToFetchXml()
        {
            _builder.Append(_mainEntity);
            _builder.Append("</fetch>");
            return _builder.ToString();
        }

        public void AddCondition(Condition condition)
        {
            _mainEntity.Conditions.Add(condition);
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