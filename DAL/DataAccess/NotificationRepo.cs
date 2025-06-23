using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public class NotificationRepo : INotificationRepo<Notification>
    {
        public Notification AddNotification(Notification notification)
        {
            using (var dbContext = new EasypayContext())
            {
                dbContext.Notifications.Add(notification);
                dbContext.SaveChanges();
                return notification;
            }
        }

        public List<Notification> GetNotificationsByEmployeeId(int employeeId)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Notifications.Where(n => n.EmployeeId == employeeId).ToList();
            }
        }
    }
}