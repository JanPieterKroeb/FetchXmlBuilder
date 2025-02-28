using FetchXmlBuilder.Interfaces.Operations;

namespace FetchXmlBuilder.Interfaces;

public interface IFetchXmlQueryMethods<T> : IAttributeOperation<IFetchXmlQueryMethods<T>, T>, IToFetchXmlStringOperation;