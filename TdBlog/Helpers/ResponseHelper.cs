using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TdBlog.Models.ResponseModels;

namespace TdBlog.Helpers
{
    public static class ResponseHelper
    {
        public static async Task<ListResponseModel<T>> AsListAsync<T>(IQueryable<T> items, PagingRequestParams pagingRequestParams) where T : IResponseModel
        {
            if (items is not IOrderedQueryable<T>)
                items = items.OrderBy(i => i.UpdatedAt ?? i.CreatedAt);

            var responseItems = await items
                .Skip(pagingRequestParams.PageNumber * pagingRequestParams.ItemPerPage)
                .Take(pagingRequestParams.ItemPerPage)
                .ToArrayAsync();

            return new ListResponseModel<T>(responseItems);
        }
    }
}