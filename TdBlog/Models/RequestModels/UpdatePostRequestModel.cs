using System;

namespace TdBlog.Models.RequestModels
{
    public class UpdatePostRequestModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}