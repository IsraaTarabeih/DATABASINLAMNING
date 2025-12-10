using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASINLÄMNING.Models
{
    /// <summary>
    /// Represents a customer in the system, including name, email and city.
    /// Each customer can have multiple orders.
    /// </summary>
    public class Customer
    {
        // PK
        public int CustomerId { get; set; }

        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        // Navigation - a customer can have many orders.
        public List<Order> Orders { get; set; } = new();
    }
}
