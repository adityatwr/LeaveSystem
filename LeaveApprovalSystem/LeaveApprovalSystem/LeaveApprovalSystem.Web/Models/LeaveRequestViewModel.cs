namespace LeaveApprovalSystem.Web.Models
{
    public class LeaveRequestViewModel
    {
        public int Id { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string? ManagerRemarks { get; set; }
    }
}
