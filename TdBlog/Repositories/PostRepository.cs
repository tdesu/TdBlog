using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TdBlog.Models;

namespace TdBlog.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<Post> GetByTitleAsync(string title);
        Task<Post> GetByTitleOrDefaultAsync(string title);
    }

    public class PostRepository : RepositoryBase<Post, Guid>, IPostRepository
    {
        public PostRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Post> GetByTitleAsync(string title)
        {
            return await SingleAsync(p => p.Title == title);
        }

        public async Task<Post> GetByTitleOrDefaultAsync(string title)
        {
            return await SingleOrDefaultAsync(p => p.Title == title);
        }
    }
}