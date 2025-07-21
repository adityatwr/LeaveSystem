using LeaveApprovalSystem.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveApprovalSystem.Web.Helper
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetLeaveTypeSelectList()
        {
            return Enum.GetValues(typeof(LeaveType))
                       .Cast<LeaveType>()
                       .Select(e => new SelectListItem
                       {
                           Value = ((int)e).ToString(),
                           Text = e.ToString()
                       }).ToList();
        }
    }

}
