using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        IUser? GetUserById(Guid id);
        bool CreateUser(IUser user);
        IUser? GetUserByEmail(string email);
        List<IUser> GetUsers();
    }
}