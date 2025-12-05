using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
    public class Product
    {
        // PK
        public int ProductId { get; set; }

        // FK -> Category
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        // Navigation - en Product kan finnas i flera OrderRows.
        public List<OrderRow> OrderRows { get; set; } = new();

        // Navigation - referens till den Category som den specifika product tillhör.
        public Category? Category { get; set; }
    }
}
