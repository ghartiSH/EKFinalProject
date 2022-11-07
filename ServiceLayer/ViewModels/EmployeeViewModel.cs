namespace ServiceLayer.ViewModels
{
    public class EmployeeViewModel
    {
        public int PeopleId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public int PositionId { get; set; }
        public string? PositionName { get; set; }

        public int EmployeeId { get; set; }
        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public string? ViewStartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ViewEndDate { get; set; }
        public string? EmployeeCode { get; set; }

        public int EmployeeHistoryId { get; set; }
    }
}
