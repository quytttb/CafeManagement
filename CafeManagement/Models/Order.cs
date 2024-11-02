using System.ComponentModel.DataAnnotations;
using CafeManagement.Models.Enums;

namespace CafeManagement.Models;

// Cấu trúc dữ liệu của từng đơn hàng
public class Order
{
    public int OrderId { get; set; }
    public int StoreId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    [Required]
    [Range(0, 999999.99)]
    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    // Navigation properties
    public virtual Store Store { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}