using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveApprovalSystem.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public int? ManagerId { get; set; }
        public int Role { get; set; }
    }
}
