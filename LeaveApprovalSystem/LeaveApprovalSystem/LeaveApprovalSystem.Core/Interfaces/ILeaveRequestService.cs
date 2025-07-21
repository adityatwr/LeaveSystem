using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveApprovalSystem.Core.Entities;

namespace LeaveApprovalSystem.Core.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsForEmployeeAsync(int employeeId);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsAllAsync();
        Task<LeaveRequest> GetLeaveRequestByIdAsync(int id);
        Task RequestLeaveAsync(LeaveRequest leaveRequest);
        Task EditLeaveRequestAsync(int id, DateTime newStart, DateTime newEnd);
        Task CancelLeaveRequestAsync(int id, int employeeId);
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsForManagerAsync(int managerId);
        Task ApproveLeaveAsync(int leaveRequestId, int managerId, string remarks);
        Task RejectLeaveAsync(int leaveRequestId, int managerId, string remarks);
    }
}
