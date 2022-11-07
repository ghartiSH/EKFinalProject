namespace ServiceLayer.ViewModels
{
    public class EmployeeHistoryViewModel
    {
        public int EmployeeHistoryId { get; set; }
        public int EmployeeId { get; set; }
        public int PositionId { get; set; }
        public string? EmployeeName { get; set; }
        public string? PositionName { get; set; }
        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public string? ViewStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ViewEndDate { get; set; }
    }
}
