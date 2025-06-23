using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
   
        public interface IAuditLogRepo<T>
        {
            T AddLog(T log);
            List<T> GetAllLogs();
        }
    
}