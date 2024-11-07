using Microsoft.AspNetCore.Mvc;
using CafeManagement.Models;
using CafeManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Employee
        public IActionResult Index()
        {
            // Lấy danh sách nhân viên, bao gồm thông tin cửa hàng và chức vụ
            var employees = _context.Employees
                .Include(e => e.Store)
                .Include(e => e.Role)
                .ToList();
            return View(employees);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            // Lấy danh sách cửa hàng và chức vụ để hiển thị trong dropdownlist
            ViewBag.Stores = new SelectList(_context.Stores, "StoreId", "StoreName");
            ViewBag.Roles = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            _logger.LogInformation($"Attempting to create employee: {employee.Name}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    _logger.LogInformation($"Employee {employee.Name} created successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating employee: {ex.Message}");
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

            // Đảm bảo rằng danh sách cửa hàng và chức vụ vẫn được hiển thị trong dropdownlist
            ViewBag.Stores = new SelectList(_context.Stores, "StoreId", "StoreName");
            ViewBag.Roles = new SelectList(_context.Roles, "RoleId", "RoleName");


            return View(employee);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            // Lấy danh sách cửa hàng và chức vụ để hiển thị trong dropdownlist
            ViewBag.Stores = new SelectList(_context.Stores, "StoreId", "StoreName", employee.StoreId);
            ViewBag.Roles = new SelectList(_context.Roles, "RoleId", "RoleName", employee.RoleId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            _logger.LogInformation($"Attempting to edit employee: {employee.Name}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    _context.SaveChanges();
                    _logger.LogInformation($"Employee {employee.Name} edited successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError($"Concurrency error editing employee: {employee.Name}");
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error editing employee: {ex.Message}");
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

            return View(employee);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}