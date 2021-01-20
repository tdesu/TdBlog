using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TdBlog.Shared
{
    public class Result
    {
        public bool Success => !ErrorMessages.Any();
        public bool Fail => ErrorMessages.Any();
        public IReadOnlyCollection<string> ErrorMessages { get; set; }

        public Result()
        {
        }

        public Result(IReadOnlyCollection<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public Result(string errorMessage)
        {
            ErrorMessages = new[] { errorMessage };
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public Result()
        {
        }

        public Result(T value = default, IReadOnlyCollection<string> errorMessages = null)
        {
            Value = value;
            ErrorMessages = errorMessages ?? Array.Empty<string>();
        }
    }
}