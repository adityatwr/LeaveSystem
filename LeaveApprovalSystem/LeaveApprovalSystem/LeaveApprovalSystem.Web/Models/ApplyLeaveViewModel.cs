using System.ComponentModel.DataAnnotations;
using LeaveApprovalSystem.Core.Attribute;

namespace LeaveApprovalSystem.Web.Models
{
    public class ApplyLeaveViewModel
    {
        [Required(ErrorMessage = "Leave Type is required.")]
        public string LeaveType { get; set; } = "Annual";

        [Required]
        [NoWeekend(ErrorMessage = "Start date cannot be a weekend.")]
        [NoPastDate(ErrorMessage = "Start date cannot be in the past.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [NoWeekend(ErrorMessage = "End date cannot be a weekend.")]
        [NoPastDate(ErrorMessage = "End date cannot be in the past.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
