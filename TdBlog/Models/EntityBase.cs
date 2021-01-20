using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TdBlog.Models
{
    public abstract class EntityBase<T>
    {
        public T Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsNew()
        {
            if (typeof(T) == typeof(Guid))
                return (Guid) (object) Id == Guid.Empty;

            return Id.Equals(default);
        }
    }
}