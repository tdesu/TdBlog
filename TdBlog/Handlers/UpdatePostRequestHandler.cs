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
    public class UpdatePostRequest : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }

    public class UpdatePostRequestHandler : IRequestHandler<UpdatePostRequest, Result>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePostRequestHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);

            post.Title = request.Title;
            post.Header = request.Header;
            post.Body = request.Body;

            _postRepository.Save(post);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new Result();
        }
    }
}