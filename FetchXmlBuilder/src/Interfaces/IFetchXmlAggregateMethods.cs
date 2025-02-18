using FetchXmlBuilder.Interfaces.Operations;

namespace FetchXmlBuilder.Interfaces;

public interface IFetchXmlAggregateMethods<T>
    : IAggregateOperation<IFetchXmlQueryMethods<T>, T>, IToFetchXmlStringOperation;