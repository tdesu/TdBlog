using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TdBlog.Shared
{
    public static class DateTimeOffsetExtensions
    {
        public static bool IsEmpty(this DateTimeOffset self)
        {
            return self == DateTimeOffset.MinValue;
        }
    }
}