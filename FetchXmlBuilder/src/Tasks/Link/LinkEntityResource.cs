using System;
using System.Linq.Expressions;

namespace FetchXmlBuilder.Tasks.Link
{
    public class LinkEntityResource<T> : ILinkEntityResource<T>
    {
        public ILinkEntityToFetchXmlBuilder<TLinkEntity> For<TLinkEntity>(Expression<Func<T, object>> queryLink)
        {
            throw new NotImplementedException();
        }
    }
}