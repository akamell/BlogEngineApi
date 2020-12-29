using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BlogEngineApi.Posts.Domain;

public interface IPostGetService
{
    IEnumerable<Post> GetByStatus(int status);
    IEnumerable<PostPendingResponse> GetPendingApproval();
    Task<IEnumerable<Post>> GetAll();
    Task<Post> Get(int id);
}