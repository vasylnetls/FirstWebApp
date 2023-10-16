using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Index(nameof(Lang), nameof(WeekDay), IsUnique = true)]
    internal class Day
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public Langs Lang { get; set; }
        public WeekDay WeekDay { get; set; }
        [Required]
        [StringLength(20)]
        public string? Value { get; set; }
    }
}