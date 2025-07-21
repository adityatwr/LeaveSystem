using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveApprovalSystem.Core.Enums;

namespace LeaveApprovalSystem.Core.Entities
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string? ManagerRemarks { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime RequestedOn { get; set; }
        public int ApproverId { get; set; }
    }
}
