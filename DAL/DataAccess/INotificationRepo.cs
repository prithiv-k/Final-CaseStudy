using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface INotificationRepo<T>
    {
        T AddNotification(T notification);
        List<T> GetNotificationsByEmployeeId(int employeeId);
    }
}