using Microsoft.AspNetCore.Mvc;
using static WebApplication1.Pages.UserEditModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.PageModels
{
    public class UserEdit
    {
        [HiddenInput] public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Name", Prompt = "Enter your name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Surname", Prompt = "Enter your surname")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter your email")]
        public string Email { get; set; }

        public AddressModel? Address { get; set; }

        [Required]
        [Range(18, 99)]
        [Display(Name = "Age", Prompt = "Enter your age")]
        public int Age { get; set; }

        [MaxLength(512)]
        [Display(Name = "Description", Prompt = "Enter your description")]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        [ReadOnly(true)]
        public string? LastUpdate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(16)]
        [Display(Name = "Password", Prompt = "Enter your password")]
        public string Password { get; set; }
    }
}
