using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

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
                    return CastToIUser(user);
                }
            }

            return null;
        }

        public bool CreateUser(IUser user)
        {
            try
            {
                Models.Address? address = null;
                if (user.Address != null)
                {
                    address = new Models.Address()
                    {
                        Id = user.Address.Id,
                        City = user.Address.City,
                        Street = user.Address.Street,
                        Index = user.Address.Index
                    };
                }

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
                    Password = user.Password,
                    Address = address
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

        public IUser? GetUserByEmail(string email)
        {
            var users = _dbContext.Users;

            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    return CastToIUser(user);
                }
            }

            return null;
        }

        public List<IUser> GetUsers()
        {
            var users = _dbContext.Users.Include(x => x.Address);
            List<IUser> usersList = new List<IUser>();

            foreach (var user in users)
            {
                usersList.Add(CastToIUser(user));
            }

            return usersList;
        }

        private Address? GetAddress(Models.Address? address)
        {
            if (address == null) return null;

            return new Address()
            {
                Id = address.Id,
                City = address.City,
                Street = address.Street,
                Index = address.Index
            };
        }

        private IUser? CastToIUser(Models.User? user)
        {
            if (user == null) return null;
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
                Password = user.Password,
                Address = GetAddress(user.Address)
            };
        }
    }
}
