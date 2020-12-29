using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BlogEngineApi.Posts.Domain;

namespace BlogEngineApi.Posts.Application
{
    public class PostDeleteService : IPostDeleteService
    {
        private readonly IConfiguration _config;
        private readonly IPostRepository _repository;

        public PostDeleteService(IConfiguration config, IPostRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public async Task<Post> Delete(int id)
        {
            var post = await this._repository.GetById(id);
            if (post == null)
            {
                throw new Exception("Post not found");
            }
            int result = await this._repository.Remove(post);
            if (result == 1)
                return post;
            else
                throw new Exception("cannot be removed");
        }
    }
}