using FetchXmlBuilder.Interfaces.Operations;

namespace FetchXmlBuilder.Interfaces;

public interface IFetchXmlQueryMethods<T> : IOperations<IFetchXmlQueryMethods<T>, T>, IToFetchXmlStringOperation;