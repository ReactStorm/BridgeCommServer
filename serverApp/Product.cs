using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public string imageUrl { get; set; }
    }
}
