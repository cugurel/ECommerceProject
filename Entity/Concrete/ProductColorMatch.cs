using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class ProductColorMatch
    {
        public int Id { get; set; } 
        public int ProductId { get; set; } 
        public int ColorId { get; set; } 
        public bool Status { get; set; } 
        public string ImagePath { get; set; } 
        public string ColorName { get; set; } 
    }
}
