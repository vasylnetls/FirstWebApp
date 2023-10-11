using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repository;

namespace DataBase
{
    public class UserRepository : IUserRepository
    {
        protected readonly MyAppDbContext _dbContext;

        public UserRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUser? GetUserById(Guid id)
        {
            var users = _dbContext.Users;

            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return new User
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Age = user.Age,
                        Description = user.Description,
                        CreateDate = user.CreateDate,
                        UpdateDate = user.UpdateDate,
                        Password = user.Password
                    };
                }
            }

            return null;
        }

        public bool CreateUser(IUser user)
        {
            try
            {
                _dbContext.Users.Add(new Models.User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Age = user.Age,
                    Description = user.Description,
                    CreateDate = user.CreateDate,
                    UpdateDate = user.UpdateDate,
                    Password = user.Password
                });

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public User? GetUserByEmail(string email)
        {
            var users = _dbContext.Users;

            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    return new User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Age = user.Age,
                        Description = user.Description,
                        CreateDate = user.CreateDate,
                        UpdateDate = user.UpdateDate,
                        Password = user.Password
                    };
                }
            }

            return null;
        }

        public List<User> GetUsers()
        {
            var users = _dbContext.Users;
            List<User> usersList = new List<User>();

            foreach (var user in users)
            {
                usersList.Add(new User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Age = user.Age,
                        Description = user.Description,
                        CreateDate = user.CreateDate,
                        UpdateDate = user.UpdateDate,
                        Password = user.Password
                    }
                );
            }

            return usersList;
        }
    }
}