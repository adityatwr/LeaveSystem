using LeaveApprovalSystem.Core.Entities;
using LeaveApprovalSystem.Core.Enums;
using LeaveApprovalSystem.Core.Interfaces;
using LeaveApprovalSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveApprovalSystem.Web.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly ILeaveRequestService _leaveRequestService;

        public ManagerController(ILogger<ManagerController> logger, ILeaveRequestService leaveRequestService)
        {
            _logger = logger;
            _leaveRequestService = leaveRequestService;
        }

        public async Task<ActionResult> PendingLeaves()
        {
            var vm = GetLeaveRequestByStatus([(int)LeaveStatus.Pending]);
            return View(vm);
        }

        public async Task<ActionResult> ApprovedLeaves()
        {
            var vm = GetLeaveRequestByStatus(
            [ (int)LeaveStatus.Approved,
              (int)LeaveStatus.Rejected,
              (int)LeaveStatus.Cancelled
            ]);
            return View(vm);
        }

        // 2. Approve
        [HttpPost]
        public async Task<ActionResult> Approve(int id, string remarks)
        {
            int managerId = GetCurrentUserId();
            await _leaveRequestService.ApproveLeaveAsync(id, managerId, remarks ?? "");
            return RedirectToAction("PendingLeaves");
        }

        // 3. Reject
        [HttpPost]
        public async Task<ActionResult> Reject(int id, string remarks)
        {
            int managerId = GetCurrentUserId();
            await _leaveRequestService.RejectLeaveAsync(id, managerId, remarks ?? "");
            return RedirectToAction("PendingLeaves");
        }

        private IEnumerable<ManagerLeaveRequestViewModel> GetLeaveRequestByStatus(int[] status)
        {
            int managerId = GetCurrentUserId();
            IEnumerable<LeaveRequest> requests = null;
            if (HttpContext.Session.GetString("Role") == "2")
                requests = _leaveRequestService.GetLeaveRequestsAllAsync().Result;
            else
                requests = _leaveRequestService.GetLeaveRequestsForManagerAsync(managerId).Result;

            var vm = requests.Where(r => status.Contains(r.Status)).Select(lr => new ManagerLeaveRequestViewModel
            {
                Id = lr.Id,
                EmployeeName = lr?.Employee?.FullName,
                LeaveType = ((LeaveType)lr.LeaveType).ToString(),
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Status = ((LeaveStatus)lr.Status).ToString(),
                ManagerRemarks = lr.ManagerRemarks
            });

            return vm;
        }

        private int GetCurrentUserId()
        {
            return HttpContext.Session.GetInt32("EmployeeId") ?? 0;
        }
    }
}
