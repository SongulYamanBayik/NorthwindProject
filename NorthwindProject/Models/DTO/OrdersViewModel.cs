namespace NorthwindProject.Models.DTO
{
    public class OrdersViewModel
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? ContactName { get; set; }
        public string? EmployeeName { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
