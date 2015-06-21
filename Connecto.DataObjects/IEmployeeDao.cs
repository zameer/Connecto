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

        Tuple<IList<Employee>, int> GetEmployees(FilterCriteria filter);
        IList<Employee> GetEmployees();
        IList<Person> GetPeople(); 
        Employee GetEmployeeById(int id);
        int DeleteEmployee(int id, int deletedBy);
        int AddEmployee(Employee employee);
        bool EditEmployee(Employee employee);
    }
}
