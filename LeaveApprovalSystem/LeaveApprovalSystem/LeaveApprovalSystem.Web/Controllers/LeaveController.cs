using LeaveApprovalSystem.Core.Entities;
using LeaveApprovalSystem.Core.Enums;
using LeaveApprovalSystem.Core.Interfaces;
using LeaveApprovalSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveApprovalSystem.Web.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILogger<LeaveController> _logger;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IEmployeeService _employeeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveController(ILogger<LeaveController> logger,ILeaveRequestService leaveRequestService, IEmployeeService employeeService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _leaveRequestService = leaveRequestService;
            _employeeService = employeeService;
            _httpContextAccessor = httpContextAccessor;
        }

        // 1. List current user's leave requests
        public async Task<ActionResult> MyLeaves()
        {
            int employeeId = GetCurrentUserId(); // Implement according to your auth system
            var leaveRequests = await _leaveRequestService.GetLeaveRequestsForEmployeeAsync(employeeId);

            var viewModel = leaveRequests.Select(lr => new LeaveRequestViewModel
            {
                Id = lr.Id,
                LeaveType = ((LeaveType)lr.LeaveType).ToString(),
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Status = ((LeaveStatus)lr.Status).ToString(),
                ManagerRemarks = lr.ManagerRemarks
            });

            return View(viewModel);
        }

        // 2. GET: Apply for leave
        public ActionResult Apply()
        {
            return View(new ApplyLeaveViewModel()
            {
                StartDate = DateTime.Now.Date,
                EndDate= DateTime.Now.Date
            });
        }

        // 3. POST: Apply for leave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Apply(ApplyLeaveViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int employeeId = GetCurrentUserId();
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

            var leaveRequest = new LeaveRequest
            {
                EmployeeId = employeeId,
                LeaveType = Convert.ToInt32(model.LeaveType),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = 0,
                RequestedOn = DateTime.Now,
                ApproverId = HttpContext.Session.GetInt32("ApproverId") ?? 0
            };

            await _leaveRequestService.RequestLeaveAsync(leaveRequest);

            return RedirectToAction("MyLeaves");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var leave = await _leaveRequestService.GetLeaveRequestByIdAsync(id);
            if (leave.Status != 0) return NotFound();

            var vm = new ApplyLeaveViewModel
            {
                LeaveType = leave.LeaveType.ToString(),
                StartDate = leave.StartDate,
                EndDate = leave.EndDate
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ApplyLeaveViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _leaveRequestService.EditLeaveRequestAsync(id, model.StartDate, model.EndDate);

            return RedirectToAction("MyLeaves");
        }


        public async Task<ActionResult> Cancel(int id)
        {
            await _leaveRequestService.CancelLeaveRequestAsync(id, GetCurrentUserId());
            return RedirectToAction("MyLeaves");
        }


        // Helper to get current user/employee Id (mimicking auth)
        private int GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("EmployeeId") ?? 0;
        }


    }

}
