using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
    /// <summary>
    /// Represents a customer order with date and status. 
    /// Each order belongs to one customer and can contain multiple order rows. 
    /// </summary>
    public class Order
    {
        // PK
        public int OrderId { get; set; }

        // FK -> Customer
        public int CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, MaxLength(100)]
        public string Status { get; set; } = string.Empty;

        // Navigation - reference to the customer who owns the order.
        public Customer? Customer { get; set; }

        // Navigation - an order can have many order rows. 
        public List<OrderRow> OrderRows { get; set; } = new();
    }
}
