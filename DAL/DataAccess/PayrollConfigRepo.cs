using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public class PayrollConfigRepo : IPayrollConfigRepo<PayrollConfig>
    {
        public PayrollConfig AddOrUpdateConfig(PayrollConfig config)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.PayrollConfigs.FirstOrDefault(p => p.EmployeeId == config.EmployeeId);
                if (existing != null)
                {
                    existing.Allowances = config.Allowances;
                    existing.Deductions = config.Deductions;
                    existing.TaxRate = config.TaxRate;
                }
                else
                {
                    dbContext.PayrollConfigs.Add(config);
                }
                dbContext.SaveChanges();
                return config;
            }
        }

        public PayrollConfig GetConfigByEmployeeId(int employeeId)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.PayrollConfigs.FirstOrDefault(p => p.EmployeeId == employeeId);
            }
        }
    }
}