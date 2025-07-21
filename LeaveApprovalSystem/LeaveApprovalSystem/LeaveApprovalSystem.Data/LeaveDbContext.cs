using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LeaveApprovalSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using LeaveApprovalSystem.Core.Enums;

namespace LeaveApprovalSystem.Data
{
    public class LeaveDbContext : DbContext
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaveRequest>()
                .Property(l => l.LeaveType)
                .HasConversion<string>();

            modelBuilder.Entity<LeaveRequest>()
                .Property(l => l.Status)
                .HasConversion<string>();
        }
    }
}
