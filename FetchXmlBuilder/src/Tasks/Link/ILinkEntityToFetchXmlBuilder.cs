using FetchXmlBuilder.Interfaces;

namespace FetchXmlBuilder.Tasks.Link
{
    public interface ILinkEntityToFetchXmlBuilder<T> : IFetchXmlConditionCollection<ILinkEntityToFetchXmlBuilder<T>, T>
    {
    }
}