using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LeaveApprovalSystem.Web.Models;
using LeaveApprovalSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using LeaveApprovalSystem.Core.Entities;

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
        HttpContext.Session.SetInt32("EmployeeId", 0);
        HttpContext.Session.SetString("EmployeeName", string.Empty);
        HttpContext.Session.SetInt32("ApproverId", 0);
        HttpContext.Session.SetString("Role", string.Empty);
        return View(new LoginViewModel { });
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if (!string.IsNullOrEmpty(model.EmployeeId))
        {
            var employee = await _employeeService.GetEmployeeByEmployeeIdAsync(model.EmployeeId);
            if (model.Password != "P@ssw0rd123")
                ModelState.AddModelError("", $"Incorrect password supplied.");
            else if (employee != null)
            {
                // Store info in session
                HttpContext.Session.SetInt32("EmployeeId", employee.Id);
                HttpContext.Session.SetString("EmployeeName", employee.FullName);
                HttpContext.Session.SetInt32("ApproverId", employee?.ManagerId ?? 0);
                HttpContext.Session.SetString("Role", employee?.Role.ToString());

                model.EmployeeName = employee.FullName;
            }
            else
                ModelState.AddModelError("", $"Employee not found for given id {model.EmployeeId}.");

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
