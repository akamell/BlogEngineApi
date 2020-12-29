public interface IUserService
{
    AuthenticationResponse DoLogin(UserRequest user);
}