using System.Threading.Tasks;
using BlogEngineApi.Posts.Domain;

public interface IPostDeleteService
{
    Task<Post> Delete(int id);
}