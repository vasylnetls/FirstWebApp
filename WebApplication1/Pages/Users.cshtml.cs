using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;
        public List<User> Users { get; set; }
        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            Users = _userService.GetUsers();
        }
    }
}
