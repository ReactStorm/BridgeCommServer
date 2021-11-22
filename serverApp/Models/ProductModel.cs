using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public bool Available { get; set; }
    }
}
