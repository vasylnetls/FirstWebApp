using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.PageModels
{
    public class UserModel : IUser
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
        public Address? Address { get; set; }
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
}
