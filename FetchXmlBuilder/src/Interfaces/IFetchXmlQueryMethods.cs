using FetchXmlBuilder.Interfaces.Operations;

namespace FetchXmlBuilder.Interfaces;

public interface IFetchXmlQueryMethods<T> : IFilterOperation<IFetchXmlQueryMethods<T>, T>, IToFetchXmlStringOperation;