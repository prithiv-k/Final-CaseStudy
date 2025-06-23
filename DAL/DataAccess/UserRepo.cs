using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.DataAccess
{
    public class UserRepo : IUserRepo<User>
    {
        public User AddUser(User user)
        {
            using (var dbContext = new EasypayContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return user;
            }
        }

        public User ValidateUser(User user)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password && u.Role == user.Role);
            }
        }

        public List<User> GetAllUsers()
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Users.ToList();
            }
        }
    }
}