using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    internal class Image : IImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Data { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
