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
            var users = _dbContext.Users.Include(x => x.Address);

            return CastToIUser(users.FirstOrDefault(user => user.Id == id));
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

        public bool UpdateUser(IUser user)
        {
            try
            {
                var dbUser = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
                if (dbUser == null)
                {
                    return false;
                }
                dbUser.Name = user.Name;
                dbUser.Age = user.Age;
                dbUser.UpdateDate = DateTime.Now;
                //dbUser.Password = user.Password;
                if (user.Address != null)
                {
                    var dbAddress = _dbContext.Addresses.FirstOrDefault(x => x.Id == user.Address.Id);
                    if (dbAddress != null && dbAddress.Id != 0)
                    {
                        dbAddress.Street = user.Address.Street;
                        dbAddress.City = user.Address.City;
                        dbAddress.Index = user.Address.Index;
                    }
                    else
                    {
                        dbUser.Address = new Models.Address
                        {
                            Street = user.Address.Street,
                            City = user.Address.City,
                            Index = user.Address.Index,
                        };
                    }
                }

                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IUser? GetUserByEmail(string email)
        {
            var users = _dbContext.Users;

            return CastToIUser(users.FirstOrDefault(user => user.Email == email));

        }

        public List<IUser?> GetUsers()
        {
            var users = _dbContext.Users.Include(x => x.Address).ToList();

            return users.Select(CastToIUser).ToList();
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
