using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Models;

// Cấu trúc dữ liệu của từng role
public class Role
{
    public int RoleId { get; set; }

    [Required]
    [StringLength(50)]
    public string RoleName { get; set; }

    public bool CanManageEmployees { get; set; }
    public bool CanManageProducts { get; set; }
    public bool CanManageOrders { get; set; }
    public bool IsAdmin { get; set; }

    // Navigation property
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}