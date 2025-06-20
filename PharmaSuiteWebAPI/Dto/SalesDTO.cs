namespace PharmaSuiteWebAPI.Dto
{
    public class SalesDTO
    {
        public DateTime SaleDate { get; set; }
        public string? CustomerName { get; set; }
        public double TotalAmount { get; set; }
        public List<SaleItemDTO> SaleItems { get; set; }
    }
}
