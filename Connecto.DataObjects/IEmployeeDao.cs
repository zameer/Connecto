using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Employee
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IEmployeeDao
    {
        /// <summary>
        /// Gets List of Employee
        /// </summary>
        /// <returns>List of Employee</returns>
        IList<Employee> GetEmployees();

        /// <summary>
        /// Get specific Employee
        /// </summary>
        /// <param name="id">Unique employee identifier</param>
        /// <returns>Specific employee Details</returns> 
        Employee GetEmployeeById(int id);

        /// <summary>
        /// Remove specific employee
        /// </summary>
        /// <param name="id">Unique employee identifier</param>
        /// <returns>No of employee Deleted</returns>
        int DeleteEmployee(int id = 0);

        /// <summary>
        /// Add specific employee
        /// </summary>
        /// <param name="id">Unique employee identifier</param>
        /// <returns>Employee ID</returns>
        int AddEmployee(Employee employee);
    }
}
