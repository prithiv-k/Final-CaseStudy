using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using DAL.Models;

namespace DAL.DataAccess
{
    public class AuditLogRepo : IAuditLogRepo<AuditLog>
    {
        public AuditLog AddLog(AuditLog log)
        {
            using (var dbContext = new EasypayContext())
            {
                dbContext.AuditLogs.Add(log);
                dbContext.SaveChanges();
                return log;
            }
        }

        public List<AuditLog> GetAllLogs()
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.AuditLogs.ToList();
            }
        }
    }
}