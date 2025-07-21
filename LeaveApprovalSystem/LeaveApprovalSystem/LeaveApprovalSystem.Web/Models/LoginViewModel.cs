namespace LeaveApprovalSystem.Web.Models
{
    public class LoginViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ApproverId { get; set; }
        public bool IsApprover { get; set; }
    }
}
