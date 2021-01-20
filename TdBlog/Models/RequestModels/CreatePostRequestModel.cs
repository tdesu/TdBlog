using System;

namespace TdBlog.Models.RequestModels
{
    public class CreatePostRequestModel
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}