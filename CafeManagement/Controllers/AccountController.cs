using CafeManagement.Models;
using CafeManagement.Models.ViewModel;
using CafeManagement.Models.ViewModel.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers;

// Quản lý tài khoản người dùng
public class AccountController : Controller
{
    // Quản lý người dùng
    private readonly UserManager<ApplicationUser> _userManager;
    // Quản lý đăng nhập
    private readonly SignInManager<ApplicationUser> _signInManager;

    // Constructor nhận vào UserManager và SignInManager
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Phương thức GET cho trang đăng nhập
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // Phương thức POST cho trang đăng nhập
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Thực hiện đăng nhập
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }

    // Phương thức GET cho trang đăng ký
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Phương thức POST cho trang đăng ký
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Tạo người dùng mới
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    // Phương thức POST cho đăng xuất
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    // Phương thức cho truy cập bị từ chối
    public IActionResult AccessDenied()
    {
        return View();
    }
}