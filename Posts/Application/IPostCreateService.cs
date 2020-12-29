using System.Threading.Tasks;

public interface IPostCreateService
{
    Task<int> Create(PostRequest request, string author);
}