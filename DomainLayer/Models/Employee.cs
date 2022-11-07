using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [ForeignKey("People")]
        public int PeopleId { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public int Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? EmployeeCode { get; set; }
        public bool IsDisabled { get; set; }
        public ICollection<EmployeeHistory>? EmployeeHistories { get; set; }
    }
}
