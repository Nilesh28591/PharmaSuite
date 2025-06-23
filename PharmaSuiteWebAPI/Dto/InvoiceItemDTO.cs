namespace PharmaSuiteWebAPI.Dto
{
    public class InvoiceItemDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double PricePerUnit { get; set; }
        public double? Discount { get; set; }
    }
}
