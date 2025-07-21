using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeaveApprovalSystem.Web.Models;
using LeaveApprovalSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LeaveApprovalSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmployeeService _employeeService;

    public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IEmployeeService employeeService)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel { });
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!string.IsNullOrEmpty(model.EmployeeId) && int.TryParse(model.EmployeeId, out int empId) && empId > 0)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(empId);

            if (employee != null)
            {
                // Store info in session
                HttpContext.Session.SetInt32("EmployeeId", employee.Id);
                HttpContext.Session.SetString("EmployeeName", employee.FullName);
                HttpContext.Session.SetInt32("ApproverId", employee?.ManagerId ?? 0);
                HttpContext.Session.SetInt32("IsApprover", model.IsApprover ? 1 : 0);

                model.EmployeeName = employee.FullName;
            }
            else
            {
                ModelState.AddModelError("", $"Employee not found for given id {model.EmployeeId}.");
            }
        }
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
