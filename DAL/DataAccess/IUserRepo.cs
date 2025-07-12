using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IUserRepo<T>
    {
        T AddUser(T user);
        T ValidateUser(T user);
        List<T> GetAllUsers();
        // or void DeleteUser(int id) depending on your preference

    }
}