using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
    /// <summary>
    /// Represents a single row in an order, containing product, quantity and unit price. 
    /// Each order row belongs to one order and one product.
    /// </summary>
    public class OrderRow
    {
        // PK
        public int OrderRowId { get; set; }

        // FK -> Order
        public int OrderId { get; set; }

        // FK -> Product
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        // Navigation - reference to the order this row belongs to.
        public Order? Order { get; set; }

        // Navigation - reference to the product in this row.
        public Product? Product { get; set; }
    }
}
