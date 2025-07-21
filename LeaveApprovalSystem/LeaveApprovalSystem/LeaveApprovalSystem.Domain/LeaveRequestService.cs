using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveApprovalSystem.Core.Entities;
using LeaveApprovalSystem.Core.Enums;
using LeaveApprovalSystem.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace LeaveApprovalSystem.Domain
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILogger<LeaveRequestService> _logger;
        private readonly IRepository<LeaveRequest> _leaveRepo;

        public LeaveRequestService(ILogger<LeaveRequestService> logger, IRepository<LeaveRequest> leaveRepo)
        {
            _logger = logger;
            _leaveRepo = leaveRepo;
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsForEmployeeAsync(int employeeId)
        {
            try
            {
                var all = await _leaveRepo.GetAllAsync();
                return all.Where(lr => lr.EmployeeId == employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting leave requests for employee {EmployeeId}", employeeId);
                return new List<LeaveRequest>();
            }
        }
        public async Task<LeaveRequest> GetLeaveRequestByIdAsync(int id)
        {
            try
            {
                return await _leaveRepo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting leave request by id {LeaveRequestId}", id);
                return null;
            }
        }

        public async Task RequestLeaveAsync(LeaveRequest leaveRequest)
        {
            try
            {
                await _leaveRepo.AddAsync(leaveRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error requesting leave for employee {EmployeeId}", leaveRequest.EmployeeId);
                throw;
            }
        }

        public async Task EditLeaveRequestAsync(int id, DateTime newStart, DateTime newEnd)
        {
            try
            {
                var leave = await _leaveRepo.GetByIdAsync(id);
                if (leave != null && leave.Status == 0)
                {
                    leave.StartDate = newStart;
                    leave.EndDate = newEnd;
                    _leaveRepo.Update(leave);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing leave request {LeaveRequestId}", id);
                throw;
            }
        }

        public async Task CancelLeaveRequestAsync(int id, int employeeId)
        {
            try
            {
                var leave = await _leaveRepo.GetByIdAsync(id);
                if (leave != null && leave.EmployeeId == employeeId && leave.Status == 0)
                {
                    leave.Status = 3; // Cancelled
                    _leaveRepo.Update(leave);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling leave request {LeaveRequestId} for employee {EmployeeId}", id, employeeId);
                throw;
            }
        }

        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsForManagerAsync(int managerId)
        {
            try
            {
                var all = await _leaveRepo.GetItemsIdAsync(managerId, "Employee");
                return all;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting leave requests for manager {ManagerId}", managerId);
                return new List<LeaveRequest>();
            }
        }

        public async Task ApproveLeaveAsync(int leaveRequestId, int managerId, string remarks)
        {
            try
            {
                var leave = await _leaveRepo.GetByIdAsync(leaveRequestId);
                if (leave != null && leave.Status == 0)
                {
                    leave.Status = 1; // Approved
                    leave.ManagerRemarks = remarks;
                    _leaveRepo.Update(leave);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving leave request {LeaveRequestId} by manager {ManagerId}", leaveRequestId, managerId);
                throw;
            }
        }

        public async Task RejectLeaveAsync(int leaveRequestId, int managerId, string remarks)
        {
            try
            {
                var leave = await _leaveRepo.GetByIdAsync(leaveRequestId);
                if (leave != null && leave.Status == 0)
                {
                    leave.Status = 2; // Rejected
                    leave.ManagerRemarks = remarks;
                    _leaveRepo.Update(leave);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting leave request {LeaveRequestId} by manager {ManagerId}", leaveRequestId, managerId);
                throw;
            }
        }
    }
}
