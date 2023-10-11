using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        IUser? GetUserById(Guid id);
        bool CreateUser(IUser user);
        User? GetUserByEmail(string email);
        List<User> GetUsers();
    }
}