using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class EmployeeRepository
    {
        private static readonly IEmployeeDao EmployeeDao = DataAccess.EmployeeDao;

        /// <summary>
        /// Get List of Employees
        /// </summary>
        /// <returns>IList of Employees</returns>
        public IList<Employee> GetAll()
        {
            return EmployeeDao.GetEmployees();
        }

        public IList<Person> GetPeople()
        {
            return EmployeeDao.GetPeople();
        }

        /// <summary>
        /// Get a specific Employee
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Employee ID</returns>
        public Employee GetEmployeeById(int id)
        {
            return EmployeeDao.GetEmployeeById(id);
        }

        /// <summary>
        /// Removes specific Employee
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of Employee Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return EmployeeDao.DeleteEmployee(id, deletedBy);
        }

        /// <summary>
        /// Create new Employee
        /// </summary>
        /// <param name="vendor">Create Supplier object</param>
        public void Add(Employee employee)
        {
            EmployeeDao.AddEmployee(employee);
        }

        /// <summary>
        /// Create new Employee
        /// </summary>
        /// <param name="vendor">Create Employee object</param>
        public void Edit(Employee employee)
        {
            EmployeeDao.EditEmployee(employee);
        }
    }
}