using FetchXmlBuilder.Builders;

namespace FetchXmlBuilder
{
    public class Builder
    {
        public EntityFetchXmlBuilder<T> For<T>(string entityName) => new EntityFetchXmlBuilder<T>(entityName);
    }
}