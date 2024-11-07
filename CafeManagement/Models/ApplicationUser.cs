namespace CafeManagement.Models;

// ApplicationUser để lưu thông tin người dùng
// Class này được mở rộng từ `IdentityUser` cho phép bạn thêm các thuộc tính và phương thức tùy chỉnh cụ thể cho các yêu cầu của ứng dụng.
// Vì `IdentityUser` trực tiếp giới hạn bạn với các thuộc tính và phương thức được xác định trước do ASP.NET Core Identity cung cấp
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Thêm các thuộc tính tùy chỉnh cho người dùng
}