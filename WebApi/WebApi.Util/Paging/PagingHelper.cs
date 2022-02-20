using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Util.Paging
{
    public class PagingHelper<T>
    {
        public static List<LinkInfo> GetLinks(PagedList<T> list, string routeName, IUrlHelper urlHelper)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLink(routeName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET", urlHelper));

            links.Add(CreateLink(routeName, list.PageNumber,
                           list.PageSize, "self", "GET", urlHelper));

            if (list.HasNextPage)
                links.Add(CreateLink(routeName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET", urlHelper));

            return links;
        }

        private static LinkInfo CreateLink(
          string routeName, int pageNumber, int pageSize,
          string rel, string method, IUrlHelper urlHelper)
        {
            return new LinkInfo
            {
                Href = urlHelper.Link(routeName,
                          new { PageNumber = pageNumber, PageSize = pageSize }),
                Rel = rel,
                Method = method
            };
        }
    }
}
