using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface ITimesheetRepo<T>
    {
        T AddOrUpdateTimesheet(T timesheet);
        List<T> GetTimesheetsByEmployeeId(int employeeId);
    }
}