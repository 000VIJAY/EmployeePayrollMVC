using Microsoft.EntityFrameworkCore;

namespace EmployeePayrollMVC.Models
{
    public class EmployeePayrollDbContext : DbContext
    {
        public EmployeePayrollDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmployeeDataModel> EmployeesDetails { get; set; }
    }
}
