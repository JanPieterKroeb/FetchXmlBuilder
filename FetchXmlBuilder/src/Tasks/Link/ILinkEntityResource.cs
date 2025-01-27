using System;
using System.Linq.Expressions;

namespace FetchXmlBuilder.Tasks.Link
{
    public interface ILinkEntityResource<T>
    {
        ILinkEntityToFetchXmlBuilder<TLinkEntity> For<TLinkEntity>(Expression<Func<T, object>> queryLink);
    }
}