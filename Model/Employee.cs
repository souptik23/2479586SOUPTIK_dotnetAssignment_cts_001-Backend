using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagemantDotnetV1.Model
{
    [Table("Employees")]
    public class Employee
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? Department { get; set; }
        public string? Designation { get; set; }

        public DateTime? DateOfJoining { get; set; }
        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
