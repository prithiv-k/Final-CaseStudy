using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IEmployeeRepo<T>
    {
        T AddEmployee(T employee);
        T UpdateEmployee(T employee);
        T DeleteEmployee(T employee);
        T GetEmployeeById(int id);
        List<T> GetAllEmployees();
        T GetEmployeeByEmail(string email);
    }
}