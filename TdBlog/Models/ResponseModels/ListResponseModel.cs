using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TdBlog.Models.ResponseModels
{
    public class ListResponseModel<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
        public int Count => Items.Count;

        public ListResponseModel()
        {
        }

        public ListResponseModel(T item)
        {
            Items = new[] { item };
        }

        public ListResponseModel(IReadOnlyCollection<T> items)
        {
            Items = items;
        }
    }
}