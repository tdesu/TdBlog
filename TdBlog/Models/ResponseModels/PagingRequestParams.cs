using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TdBlog.Models.ResponseModels
{
    public class PagingRequestParams
    {
        public int PageNumber { get; set; }
        public int ItemPerPage { get; set; } = 10;
    }
}