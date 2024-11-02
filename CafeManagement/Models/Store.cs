using System.ComponentModel.DataAnnotations;
using CafeManagement.Models.Enums;

namespace CafeManagement.Models;

// Cấu trúc dữ liệu của từng cửa hàng
public class Store
{
    public int StoreId { get; set; }

    [Required]
    [StringLength(100)]
    public string StoreName { get; set; }

    [Required]
    [StringLength(255)]
    public string Address { get; set; }

    [StringLength(20)]
    public string Phone { get; set; }

    // Navigation properties
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}