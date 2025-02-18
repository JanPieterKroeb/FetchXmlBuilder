using FetchXmlBuilder.Domain;

namespace FetchXmlBuilder.Builders;

internal class LinkEntityFetchXmlBuilder<TLinkEntity>(string entityName)
    : EntityFetchXmlBuilder<TLinkEntity, EntityQuery>(entityName) where TLinkEntity : EntityQuery;