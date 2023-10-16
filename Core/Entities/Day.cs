using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Entities
{
    public class Day
    {
        public int Id { get; set; }
        public Langs Lang { get; set; }
        public WeekDay WeekDay { get; set; }
        public string? Value { get; set; }
    }
}
