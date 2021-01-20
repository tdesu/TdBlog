using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoMapper;
using TdBlog.Models;
using TdBlog.Models.ResponseModels;

namespace TdBlog
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostResponseModel>();
        }
    }
}