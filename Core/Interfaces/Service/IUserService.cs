using Core.Entities;

namespace Core.Interfaces.Service;

public interface IUserService
{
    IUser? GetUserById(Guid id);
    bool CreateUser(IUser user);
    bool IsUsedEmail(string email);
    List<IUser> GetUsers();
}