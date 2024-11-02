using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Models;

// Cấu trúc dữ liệu của từng chi tiết đơn hàng
public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    [Range(0, 999999.99)]
    public decimal UnitPrice { get; set; }

    // Navigation properties
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}