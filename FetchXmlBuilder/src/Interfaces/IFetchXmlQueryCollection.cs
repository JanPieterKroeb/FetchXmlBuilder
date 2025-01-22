namespace FetchXmlBuilder.Interfaces
{
    public interface IFetchXmlQueryCollection<T> : IFetchXmlConditionCollection<IFetchXmlQueryCollection<T>, T>
    {
        string ToFetchXmlString();
    }
}