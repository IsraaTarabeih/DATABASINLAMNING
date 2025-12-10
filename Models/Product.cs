using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
    /// <summary>
    /// Represents a product with a name and price. 
    /// Each product belongs to one category and can appear in multiple order rows.
    /// </summary>
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

        // Navigation - a product can be part of many order rows.
        public List<OrderRow> OrderRows { get; set; } = new();

        // Navigation - reference to the category the product belongs to. 
        public Category? Category { get; set; }
    }
}
