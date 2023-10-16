using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [StringLength(120)]
        public string? City { get; set; }
        [StringLength(120)]
        public string? Street { get; set; }
        
        [StringLength(20)]
        [Required]
        public string? Index { get; set; }

    }
}
