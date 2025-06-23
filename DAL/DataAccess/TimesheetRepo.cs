using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public class TimesheetRepo : ITimesheetRepo<Timesheet>
    {
        public Timesheet AddOrUpdateTimesheet(Timesheet timesheet)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.Timesheets.FirstOrDefault(t => t.EmployeeId == timesheet.EmployeeId && t.Date == timesheet.Date);
                if (existing != null)
                {
                    existing.HoursWorked = timesheet.HoursWorked;
                }
                else
                {
                    dbContext.Timesheets.Add(timesheet);
                }
                dbContext.SaveChanges();
                return timesheet;
            }
        }

        public List<Timesheet> GetTimesheetsByEmployeeId(int employeeId)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Timesheets.Where(t => t.EmployeeId == employeeId).ToList();
            }
        }
    }
}