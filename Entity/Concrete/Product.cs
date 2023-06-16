using Entity.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string? Consume { get; set; }
        public string? ConsumeMethod { get; set; }
        public string? Packaging { get; set; }
        public string? Warranty { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        
    }
}
