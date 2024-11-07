using CafeManagement.Data;
using Microsoft.AspNetCore.Mvc;
using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<StoreController> _logger;
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context, ILogger<StoreController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Store
        // Phương thức này trả về danh sách các cửa hàng
        public IActionResult Index()
        {
            return View(_context.Stores.ToList());
        }

        // GET: Store/Details/5
        // Phương thức này trả về chi tiết của một cửa hàng cụ thể
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = _context.Stores
                .FirstOrDefault(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Store ID: {store.StoreId}");
            return View(store);
        }

        // GET: Store/Create
        // Phương thức này trả về view để tạo mới một cửa hàng
        public IActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
        // Phương thức này xử lý việc tạo mới một cửa hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Store store)
        {
            _logger.LogInformation($"Attempting to create store: {store.StoreName}");// Ghi log thông báo tạo mới cửa hàng

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("ModelState is valid, adding store to context");// Ghi log thông báo ModelState hợp lệ
                    _context.Stores.Add(store);
                    _context.SaveChanges();
                    _logger.LogInformation($"Store {store.StoreName} created successfully");// Ghi log tạo mới cửa hàng thành công
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating store: {ex.Message}");// Ghi log lỗi khi tạo mới cửa hàng
                    throw;
                }
            }

            _logger.LogWarning("ModelState is invalid. Errors:");// Ghi log ModelState không hợp lệ

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogWarning($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }

            return View(store);
        }

        // GET: Store/Edit/5
        // Phương thức này trả về view để chỉnh sửa thông tin một cửa hàng
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = _context.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Store ID: {store.StoreId}");

            return View(store);
        }

        // POST: Store/Edit/1 (ví dụ với id = 1)
        // Phương thức return NotFound();này xử lý việc chỉnh sửa thông tin một cửa hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Store store)
        {
            if (id != store.StoreId)
            {

            }

            _logger.LogInformation($"Attempting to edit employee: {store.StoreName}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    _context.SaveChanges();
                    _logger.LogInformation($"Store {store.StoreName} updated successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError($"Error updating store: {store.StoreName}");
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating store: {ex.Message}");
                    throw;
                }
            }

            _logger.LogWarning("ModelState is invalid. Errors:");
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogWarning($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }

            return View(store);
        }

        // GET: Store/Delete/5
        // Phương thức này trả về view để xác nhận việc xóa một cửa hàng
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = _context.Stores
                .FirstOrDefault(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Store/Delete/5
        // Phương thức này xử lý việc xóa một cửa hàng
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var store = _context.Stores.Find(id);
            _context.Stores.Remove(store);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Phương thức này kiểm tra xem một cửa hàng có tồn tại hay không
        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.StoreId == id);
        }
    }
}