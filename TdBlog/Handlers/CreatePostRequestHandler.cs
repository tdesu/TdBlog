using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TdBlog.Models;
using TdBlog.Repositories;
using TdBlog.Shared;

namespace TdBlog.Handlers
{
    public class CreatePostRequest : IRequest<Result<Post>>
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }

    public class CreatePostRequestHandler : IRequestHandler<CreatePostRequest, Result<Post>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostRequestHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Post>> Handle(CreatePostRequest request, CancellationToken cancellationToken)
        {
            var post = new Post { Body = request.Body, Header = request.Header, Title = request.Title };

            _postRepository.Save(post);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new Result<Post>(post);
        }
    }
}