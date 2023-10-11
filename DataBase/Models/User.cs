﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DataBase.Models
{
    internal class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(55)]
        public string Name { get; set; }
        [StringLength(55)]
        public string Surname { get; set; }
        [StringLength(55)]
        public string Email { get; set; }
        [Range(0, 100)]
        public int Age { get; set; }
        [StringLength(125)]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Password { get; set; }
    }
}
