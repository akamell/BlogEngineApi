using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BlogEngineApi.Posts.Domain;

public interface IPostUpdateService
{
    Task<Post> ApprovePost(int id, ApprovePostRequest approveRequest);
    Task<Post> ChangeContent(int id, PostRequest post);
}