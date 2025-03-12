using FetchXmlBuilder.Interfaces.Operations;

namespace FetchXmlBuilder.Tasks.Link;

public interface ILinkEntityToFetchXmlBuilder<T> : IOperations<ILinkEntityToFetchXmlBuilder<T>, T>
{
}