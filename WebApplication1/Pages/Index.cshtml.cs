using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Azure;
using Core.Interfaces;
using Core.Interfaces.Service;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public class User : IUser
        {
            [HiddenInput]
            public Guid Id { get; set; }
            [Required]
            [MinLength(3)]
            [StringLength(50)]
            [Display(Name = "Name", Prompt = "Enter your name")]
            public string Name { get; set; }
            [Required]
            [MinLength(3)]
            [StringLength(50)]
            [Display(Name = "Surname", Prompt = "Enter your surname")]
            public string Surname { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email", Prompt = "Enter your email")]
            public string Email { get; set; }
            [Required]
            [Range(18, 99)]
            [Display(Name = "Age", Prompt = "Enter your age")]
            public int Age { get; set; }
            [Display(Name = "Description", Prompt = "Enter your description")]
            public string? Description { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime? UpdateDate { get; set; }
            [Required]
            [MaxLength(16)]
            [DataType(DataType.Password)]
            [Display(Name = "Password", Prompt = "Enter your password")]
            public string Password { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords must be equal")]
            [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
            public string ConfirmPassword { get; set; }
        }

        [BindProperty]
        public User MyUser { get; set; }

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
                var user = new User()
                {
                    Name = MyUser.Name,
                    Surname = MyUser.Surname,
                    Age = MyUser.Age,
                    Email = MyUser.Email,
                    Description = MyUser.Description,
                    Password = MyUser.Password
                };
                if (_userService.CreateUser(user))
                {
                    Responce = new Result() { ResultType = ResultType.Success, Message = $"User {MyUser.Name} has been created" };
                }
                else
                {
                    Responce = new Result() { ResultType = ResultType.Error, Message = $"User {MyUser.Name} has not been created" };
                }
            }
            else
            {
                Responce = new Result() { ResultType = ResultType.Error, Message = "User has not been created" };
            }
        }

    }
}