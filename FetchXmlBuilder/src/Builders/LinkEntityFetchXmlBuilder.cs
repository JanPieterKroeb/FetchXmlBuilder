namespace FetchXmlBuilder.Builders
{
    internal class LinkEntityFetchXmlBuilder<TLinkEntity> : EntityFetchXmlBuilder<TLinkEntity>
    {
        public LinkEntityFetchXmlBuilder(string entityName) : base(entityName)
        {
        }
    }
}