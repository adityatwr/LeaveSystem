using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveApprovalSystem.Core.Entities;
using LeaveApprovalSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LeaveApprovalSystem.Domain
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(ILogger<EmployeeService> logger,IRepository<Employee> employeeRepo, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _employeeRepo = employeeRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await _employeeRepo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployeeByIdAsync. EmployeeId: {EmployeeId}", id);
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _employeeRepo.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllEmployeesAsync.");
                return new List<Employee>();
            }
        }
    }
}
