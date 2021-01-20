using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TdBlog.Models
{
    public class Post : EntityBase<Guid>
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}