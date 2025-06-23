using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmaSuiteWebAPI.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string Name { get; set; } = null!;       // required, non-nullable

        public string Mobile { get; set; } = null!;     // must be named Mobile, not Phone

        public string Address { get; set; } = null!;    // Address present

        public string? Email { get; set; }               // Email is optional or required? Adjust accordingly.
        public bool IsOnline { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Sale>? Sales { get; set; }   // If you want navigation property
    }
}
