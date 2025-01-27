namespace FetchXmlBuilder.Builders
{
    public class LinkEntityFetchXmlBuilder<TLinkEntity> : EntityFetchXmlBuilder<TLinkEntity>
    {
        public LinkEntityFetchXmlBuilder(string entityName) : base(entityName)
        {
        }
    }
}