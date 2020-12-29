using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using BlogEngineApi.Posts.Domain;
using BlogEngineApi.Shared.Infrastructure.Persistence.EntityFramework;

namespace BlogEngineApi.Posts.Infrastructure.Persistence
{
    public class MySqlPostRepository : IPostRepository
    {
        private readonly BlogEngineDbContext _context;

        public MySqlPostRepository(BlogEngineDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save(Post post)
        {
            if (_context.Entry(post).State == EntityState.Detached)
                await _context.AddAsync(post);
            else
                _context.Entry(post).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Remove(Post post)
        {
            _context.Remove(post);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> SearchAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public IEnumerable<Post> Filter(Expression<Func<Post, bool>> predicate)
        {
            return _context.Set<Post>().Where(predicate);
        }

        public IEnumerable<PostPendingResponse> GetPending()
        {
            return _context.Posts
            .Where(item => item.Status == 0)
            .Select(x => new PostPendingResponse
            {
                PostID = x.PostID,
                Author = x.Author,
                Publish = x.Publish
            }
            );
        }
    }
}