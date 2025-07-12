using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAccess
{
    public class PayrollConfigRepo : IPayrollConfigRepo<PayrollConfig>
    {
        public PayrollConfig AddOrUpdateConfig(PayrollConfig config)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.PayrollConfigs
                                        .FirstOrDefault(p => p.EmployeeId == config.EmployeeId);

                if (existing != null)
                {
                    // Update only the fields that can be modified
                    existing.Allowances = config.Allowances;
                    existing.Deductions = config.Deductions;
                    existing.TaxRate = config.TaxRate;

                    dbContext.PayrollConfigs.Update(existing);
                }
                else
                {
                    dbContext.PayrollConfigs.Add(config);
                }

                dbContext.SaveChanges();

                return dbContext.PayrollConfigs
                                .Include(p => p.Employee) // optional if you want to return related data
                                .FirstOrDefault(p => p.EmployeeId == config.EmployeeId);
            }
        }

        public PayrollConfig GetConfigByEmployeeId(int employeeId)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.PayrollConfigs
                                .Include(p => p.Employee) // optional if needed
                                .FirstOrDefault(p => p.EmployeeId == employeeId);
            }
        }

        public PayrollConfig GetByConfigId(int configId)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.PayrollConfigs
                                .Include(p => p.Employee) // optional if needed
                                .FirstOrDefault(p => p.ConfigId == configId);
            }
        }

        public List<PayrollConfig> GetAll()
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.PayrollConfigs
                                .Include(p => p.Employee) // if you want employee info in table
                                .ToList();
            }
        }
    }
}
