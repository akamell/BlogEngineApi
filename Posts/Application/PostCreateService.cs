using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BlogEngineApi.Posts.Domain;

namespace BlogEngineApi.Posts.Application
{
    public class PostCreateService : IPostCreateService
    {
        private readonly IConfiguration _config;
        private readonly IPostRepository _repository;

        public PostCreateService(IConfiguration config, IPostRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public Task<int> Create(PostRequest postRequest, string author)
        {
            if (string.IsNullOrEmpty(postRequest.Content))
            {
                throw new Exception("Invalid post content");
            }

            if (postRequest.Content.Length > 250)
            {
                throw new Exception("Invalid content length");
            }

            var newPost = new BlogEngineApi.Posts.Domain.Post()
            {
                Content = postRequest.Content,
                Author = author,
                Status = 0,
                Publish = DateTime.Now,
            };

            return this._repository.Save(newPost);
        }
    }
}