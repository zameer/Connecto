using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IEmployeeDao interface.
    /// </summary>
    public class EntityEmployeeDao : IEmployeeDao
    {
        public IList<Employee> GetEmployees()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var employees = context.Employees.ToList();
                return employees.Select(Mapper.Map).ToList();
            }
        }

        // get Employee by id
        public Employee GetEmployeeById(int employeeId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteEmployee(int id = 0)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Employees.FirstOrDefault(s => s.EmployeeId == id);
                context.Employees.Remove(entity);
                return context.SaveChanges();
            }
        }

        public int AddEmployee(Employee employee)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(employee);
                context.Employees.Add(entity);
                context.SaveChanges();
                return entity.EmployeeId;
            }
        }
    }
}
