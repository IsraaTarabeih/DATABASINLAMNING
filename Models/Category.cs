using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
    public class Category
    {
        // PK
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string CategoryDescription { get; set; } = string.Empty;

        // Navigation - en Category kan ha flera Products.  
        public List<Product> Products { get; set; } = new();
    }
}