using FetchXmlBuilder.Interfaces.Operations;

namespace FetchXmlBuilder.Tasks.Link;

public interface ILinkEntityToFetchXmlBuilder<T> : IFilterOperation<ILinkEntityToFetchXmlBuilder<T>, T>
{
}