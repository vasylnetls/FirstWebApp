using System.ComponentModel;
using Azure;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static WebApplication1.Pages.IndexModel;
using Microsoft.EntityFrameworkCore;
using WebApplication1.PageModels;

namespace WebApplication1.Pages
{
    public class UserEditModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty(SupportsGet = true)] public Guid Id { get; set; }
        [BindProperty] public UserEdit MyUser { get; set; }
        public Result Response { get; set; }

        public UserEditModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(Guid id)
        {
            var dbUser = _userService.GetUserById(id);
            if (dbUser != null)
            {
                var lastUpdate = string.Empty;
                if (dbUser.UpdateDate.HasValue)
                {
                    var expiredDays = (DateTime.Now - dbUser.UpdateDate.Value).Days;
                    if (expiredDays != 0)
                    {
                        lastUpdate = expiredDays + " days ago";
                    }
                    else
                    {
                        lastUpdate = "today";
                    }
                }
                MyUser = new UserEdit()
                {
                    Id = dbUser.Id,
                    Name = dbUser.Name,
                    Surname = dbUser.Surname,
                    Email = dbUser.Email,
                    Description = dbUser.Description,
                    CreateDate = dbUser.CreateDate,
                    LastUpdate = lastUpdate,
                    Age = dbUser.Age,
                    Address = dbUser.Address != null
                        ? new AddressModel()
                        {
                            Id = dbUser.Address.Id,
                            City = dbUser.Address.City,
                            Street = dbUser.Address.Street,
                            Index = dbUser.Address.Index,
                        }
                        : null,
                };
            }
            else
            {
                return BadRequest();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (MyUser != null)
            {
                var user = _userService.GetUserById(MyUser.Id);
                if (user != null)
                {
                    user.Name = MyUser.Name;
                    user.Surname = MyUser.Surname;
                    user.Age = MyUser.Age;
                    user.Description = MyUser.Description;
                    //user.Password = MyUser.Password;

                    if (MyUser.Address != null)
                    {
                        //if (user.AddressId != 0 && user.Address != null)
                        if (user.Address != null)
                        {
                            user.Address.Street = MyUser.Address.Street;
                            user.Address.City = MyUser.Address.City;
                            user.Address.Index = MyUser.Address.Index;
                        }
                        else
                        {
                            user.Address = new Address()
                            {
                                City = MyUser.Address.City,
                                Street = MyUser.Address.Street,
                                Index = MyUser.Address.Index
                            };
                        }
                    }

                    if (_userService.UpdateUser(user))
                    {
                        Response = new Result()
                            { ResultType = ResultType.Success, Message = $"IUser {MyUser.Name} has been updated" };
                    }
                    else
                    {
                        Response = new Result()
                            { ResultType = ResultType.Error, Message = $"IUser {MyUser.Name} has not been updated" };
                    }
                }
            }
            else
            {
                Response = new Result() { ResultType = ResultType.Error, Message = "IUser does not exist" };
            }

            return Page();
        }

    }
}