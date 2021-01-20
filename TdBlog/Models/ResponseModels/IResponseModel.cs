using System;

namespace TdBlog.Models.ResponseModels
{
    public interface IResponseModel
    {
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
    }

    public abstract class ResponseModelBase : IResponseModel
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}