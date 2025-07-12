using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IPayrollRepo<T>
    {
        T GeneratePayroll(T payroll);
        T AddPayroll(T payroll);
        T UpdatePayroll(T payroll);
        T DeletePayroll(T payroll);
        List<T> GetPayrollsByEmployeeId(int employeeId);
        List<T> GetAllPayrolls();
        T VerifyPayroll(int payrollId);
    }
}