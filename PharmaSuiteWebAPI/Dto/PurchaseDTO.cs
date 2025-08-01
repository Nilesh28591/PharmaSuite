﻿namespace PharmaSuiteWebAPI.Dto
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public string Name { get; set; } // Supplier Name
        public string Email { get; set; } // Supplier Email
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string CreatedBy { get; set; }
        public List<PurchaseItemDTO> Items { get; set; }
    }
}
