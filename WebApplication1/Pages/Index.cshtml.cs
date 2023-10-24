using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Azure;
using Core.Interfaces;
using Core.Interfaces.Service;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.PageModels;
using Address = Core.Entities.Address;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        

        [BindProperty]
        public UserModel MyUser { get; set; }

        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {

        }

        public Result Responce { get; set; }
        public enum ResultType
        {
            Success,
            Error
        }

        public class Result
        {
            public ResultType ResultType { get; set; }
            public string? Message { get; set; }
        }

        public void OnPost()
        {
            ModelState.Remove("MyUser.Id");
            if (string.IsNullOrEmpty(MyUser.Email) || _userService.IsUsedEmail(MyUser.Email))
            {
                ModelState.AddModelError("MyUser.Email", "This email is already used");
            }
            if (!ModelState.IsValid)
            {
                return;
            }
            if (MyUser != null)
            {
                var user = new UserModel()
                {
                    Name = MyUser.Name,
                    Surname = MyUser.Surname,
                    Age = MyUser.Age,
                    Email = MyUser.Email,
                    Description = MyUser.Description,
                    Password = MyUser.Password,
                    Address = MyUser.Address
                };
                if (_userService.CreateUser(user))
                {
                    Responce = new Result() { ResultType = ResultType.Success, Message = $"IUser {MyUser.Name} has been created" };
                }
                else
                {
                    Responce = new Result() { ResultType = ResultType.Error, Message = $"IUser {MyUser.Name} has not been created" };
                }
            }
            else
            {
                Responce = new Result() { ResultType = ResultType.Error, Message = "IUser has not been created" };
            }
        }

        public IActionResult OnPostCheckEmail(string email)
        {
            return new JsonResult(_userService.IsUsedEmail(email));
        }

    }
}