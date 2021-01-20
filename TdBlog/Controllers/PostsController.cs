using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TdBlog.Handlers;
using TdBlog.Helpers;
using TdBlog.Models;
using TdBlog.Models.RequestModels;
using TdBlog.Models.ResponseModels;
using TdBlog.Models.ResponseModels.Lists;
using TdBlog.Repositories;

namespace TdBlog.Controllers
{
    [ApiController]
    [Route("api/v7/[controller]/[action]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostsController(IPostRepository postRepository, IMapper mapper, IMediator mediator)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PostsResponseModel>> List([FromQuery] PagingRequestParams pagingRequestParams)
        {
            var posts = _postRepository.QueryAll().ProjectTo<PostResponseModel>(_mapper.ConfigurationProvider);
            return Ok(await ResponseHelper.AsListAsync(posts, pagingRequestParams));
        }

        [HttpGet]
        public async Task<ActionResult<PostResponseModel>> Get(string title)
        {
            var post = await _postRepository.GetByTitleAsync(title);
            return Ok(_mapper.Map<PostResponseModel>(post));
        }

        [HttpPost]
        public async Task<ActionResult<PostResponseModel>> Create([FromBody] CreatePostRequestModel requestModel)
        {
            var request = new CreatePostRequest { Header = requestModel.Header, Body = requestModel.Body, Title = requestModel.Title };
            var postResult = await _mediator.Send(request);

            if (postResult.Fail)
                return BadRequest(postResult.ErrorMessages);

            return Ok(_mapper.Map<PostResponseModel>(postResult.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdatePostRequestModel requestModel)
        {
            var request = new UpdatePostRequest { Id = requestModel.Id, Header = requestModel.Header, Body = requestModel.Body, Title = requestModel.Title };
            var postResult = await _mediator.Send(request);

            if (postResult.Fail)
                return BadRequest(postResult.ErrorMessages);

            return Ok();
        }
    }
}