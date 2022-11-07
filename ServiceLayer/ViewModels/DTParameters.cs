namespace ServiceLayer.ViewModels
{
    public class DTParameters
    {
        public string? Draw { get; set; }
        public string? Start { get; set; }
        public string? Length { get; set; }
        public string? SortColumn { get; set; }
        public string? SortColumnDirection { get; set; }
        public string? SearchValue { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int RecordsTotal { get; set; }
        public List<EmployeeViewModel>? EmployeeRecords { get; set; }

    }
}
