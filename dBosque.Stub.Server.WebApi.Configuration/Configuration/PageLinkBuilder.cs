using dBosque.Stub.Server.WebApi.Configuration.Extensions;
using dBosque.Stub.Server.WebApi.Configuration.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace dBosque.Stub.Server.WebApi.Configuration
{
    /// <summary>
    /// PageLink builder
    /// </summary>
    public static class PageLinkBuilder
    {
        /// <summary>
        /// Return as a headerlink
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="urlHelper"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static PagedActionResult AddLinkHeader(this PagedActionResult msg, IUrlHelper urlHelper, PagedResult result)
        {
            return AddLinkHeader(msg, urlHelper, result.routeName, result.Additional, result.pageNo, result.pageSize, result.total);
        }

        /// <summary>
        /// Create an uri for the given pagenumber
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="routeName"></param>
        /// <param name="routeValues"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private static Uri CreateUriForPage(IUrlHelper urlHelper, string routeName, IDictionary<string, object> routeValues, int pageNo, int pageSize)
        {
            return new Uri(urlHelper.Link(routeName, new RouteValueDictionary(routeValues)
            {
                {"pageNo", pageNo},
                {"pageSize", pageSize}
            }));
        }

        /// <summary>
        /// Return as a header link
        /// </summary>
        /// <returns></returns>
        public static PagedActionResult AddLinkHeader(this PagedActionResult msg, IUrlHelper urlHelper, string routeName, IDictionary<string,object> routeValues, int pageNo, int pageSize, long totalRecordCount)
        {

            // Determine total number of pages
            var pageCount = totalRecordCount > 0
                ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                : 0;

            string headerTemplate = "<{0}>; rel=\"{1}\"";
            List<string> links = new List<string>();
            links.Add(string.Format(headerTemplate, CreateUriForPage(urlHelper, routeName, routeValues, 1, pageSize), "first"));

            if (pageNo > 1)
                links.Add(string.Format(headerTemplate, CreateUriForPage(urlHelper, routeName, routeValues, pageNo - 1, pageSize), "previous"));

            if (pageNo < pageCount)
                links.Add(string.Format(headerTemplate, CreateUriForPage(urlHelper, routeName, routeValues, pageNo + 1, pageSize), "next"));

            links.Add(string.Format(headerTemplate, CreateUriForPage(urlHelper, routeName, routeValues, pageCount, pageSize), "last"));

            msg.Link = string.Join(", ", links);
            return msg;
        }
    }
}
