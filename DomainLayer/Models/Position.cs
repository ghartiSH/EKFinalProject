using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string? Name { get; set; }

        public ICollection<Employee>? Employees { get; set; }
        public ICollection<EmployeeHistory>? EmployeeHistories { get; set; }
    }
}
