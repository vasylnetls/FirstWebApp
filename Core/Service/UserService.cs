using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Service
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUser? GetUserById(Guid id)
        {
            return _userRepository.GetUserById(id);
        }

        public bool CreateUser(IUser user)
        {
            user.Id = Guid.NewGuid();
            user.CreateDate = DateTime.Now;

            return _userRepository.CreateUser(user);
        }

        public bool IsUsedEmail(string email)
        {
            return _userRepository.GetUserByEmail(email) != null;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}