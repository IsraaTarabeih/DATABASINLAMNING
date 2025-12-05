using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
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

        // Navigation - referens till Order-objektet denna OrderRow tillhör.
        public Order? Order { get; set; }

        // Navigation - referens till Product-objektet för denna OrderRow.
        public Product? Product { get; set; }
    }
}
