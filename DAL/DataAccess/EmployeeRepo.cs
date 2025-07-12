using System;
using DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class EmployeeRepo : IEmployeeRepo<Employee>
    {
        public Employee AddEmployee(Employee employee)
        {
            using (var dbContext = new EasypayContext())
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
                return employee;
            }
        }

        public Employee UpdateEmployee(Employee employee)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (existing != null)
                {
                    existing.Name = employee.Name;
                    existing.Email = employee.Email;
                    existing.Department = employee.Department;
                    existing.Role = employee.Role;
                    dbContext.SaveChanges();
                }
                return existing;
            }
        }

        public Employee DeleteEmployee(Employee employee)
        {
            using (var dbContext = new EasypayContext())
            {
                var existing = dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
                if (existing != null)
                {
                    dbContext.Employees.Remove(existing);
                    dbContext.SaveChanges();
                }
                return existing;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            }
        }

        public Employee GetEmployeeByEmail(string email)
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Employees.FirstOrDefault(e => e.Email == email);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            using (var dbContext = new EasypayContext())
            {
                return dbContext.Employees.ToList();
            }
        }
    }
}