using System.ComponentModel.DataAnnotations;

namespace CafeManagement.Models;

// Cấu trúc dữ liệu của từng sản phẩm
public class Product
{
    public int ProductId { get; set; }
    public int StoreId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [Range(0, 999999.99)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    // Navigation properties
    public virtual Store Store { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}