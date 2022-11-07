
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class People
    {
        [Key]
        public int PeopleId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
