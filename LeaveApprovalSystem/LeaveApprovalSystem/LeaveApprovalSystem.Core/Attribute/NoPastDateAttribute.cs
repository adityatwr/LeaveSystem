using System.ComponentModel.DataAnnotations;

namespace LeaveApprovalSystem.Core.Attribute
{
    public class NoPastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
                return date.Date >= DateTime.Now.Date;
            return true;
        }
    }

}
