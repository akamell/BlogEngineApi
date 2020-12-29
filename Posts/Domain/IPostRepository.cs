using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace BlogEngineApi.Posts.Domain
{
    public interface IPostRepository
    {
        Task<int> Remove(Post post);
        Task<int> Save(Post post);
        Task<IEnumerable<Post>> SearchAll();
        Task<Post> GetById(int id);
        IEnumerable<Post> Filter(Expression<Func<Post, bool>> predicate);
        IEnumerable<PostPendingResponse> GetPending();
    }
}