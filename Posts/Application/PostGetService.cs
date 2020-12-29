using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BlogEngineApi.Posts.Domain;

namespace BlogEngineApi.Posts.Application
{
    public class PostGetService : IPostGetService
    {
        private readonly IConfiguration _config;
        private readonly IPostRepository _repository;

        public PostGetService(IConfiguration config, IPostRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public Task<IEnumerable<Post>> GetAll()
        {
            return this._repository.SearchAll();
        }

        public Task<Post> Get(int id)
        {
            return this._repository.GetById(id);
        }

        public IEnumerable<Post> GetByStatus(int status)
        {
            return this._repository.Filter(item => item.Status == status);
        }

        public IEnumerable<PostPendingResponse> GetPendingApproval()
        {
            return this._repository.GetPending();
        }
    }
}