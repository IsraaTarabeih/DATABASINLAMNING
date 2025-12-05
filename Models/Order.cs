using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
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

        // Navigation - referens till den Customer som äger Ordern
        public Customer? Customer { get; set; }

        // Navigation - en Order kan ha flera OrderRows.
        public List<OrderRow> OrderRows { get; set; } = new();
    }
}
