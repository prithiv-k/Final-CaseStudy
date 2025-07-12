using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IPayrollConfigRepo<T>
    {
        T AddOrUpdateConfig(T config);
        T GetConfigByEmployeeId(int employeeId);
        List<T> GetAll();
    }
}