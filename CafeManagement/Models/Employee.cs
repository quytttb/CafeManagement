using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Models;

// Cấu trúc dữ liệu của nhân viên
public class Employee
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int RoleId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public DateTime HireDate { get; set; }

    // Navigation properties
    public virtual Store Store { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}