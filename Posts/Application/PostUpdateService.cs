using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BlogEngineApi.Posts.Domain;

namespace BlogEngineApi.Posts.Application
{
    public class PostUpdateService : IPostUpdateService
    {
        private readonly IConfiguration _config;
        private readonly IPostRepository _repository;

        public PostUpdateService(IConfiguration config, IPostRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public async Task<Post> ApprovePost(int id, ApprovePostRequest approveRequest)
        {
            var post = await this._repository.GetById(id);
            if (post == null)
            {
                throw new Exception("post not found");
            }

            if (post.Status > 0)
            {
                throw new Exception("cannot be approved");
            }

            if (approveRequest.Status > 2 || approveRequest.Status < 0)
            {
                throw new Exception("Invalid status");
            }

            if (approveRequest.Status > 0) post.Approval = DateTime.Now;

            post.Status = approveRequest.Status;

            var result = await this._repository.Save(post);
            if (result == 1)
                return post;
            else
                throw new Exception("Failed to update post");
        }

        public async Task<Post> ChangeContent(int id, PostRequest postRequest)
        {
            var post = await this._repository.GetById(id);
            if (post == null)
            {
                throw new Exception("post not found");
            }

            if (post.Status == 1)
            {
                throw new Exception("cannot be changed");
            }

            if (postRequest.Content.Length > 250)
            {
                throw new Exception("Invalid content length");
            }

            post.Content = postRequest.Content;
            post.Publish = DateTime.Now;
            post.Status = 0;
            var result = await this._repository.Save(post);
            if (result == 1)
                return post;
            else
                throw new Exception("Failed to update post");

        }
    }
}