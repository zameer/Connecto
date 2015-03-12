using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;

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
                var employees = context.Employees.Where(e => e.Status == RecordStatus.Active).ToList();
                return employees.Select(Mapper.Map).ToList();
            }
        }
        public IList<Person> GetPeople()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var personIds = context.Employees.Where(e => e.Status == RecordStatus.Active).Select(e => e.PersonId).ToArray();
                var people = context.People.Where(e => !personIds.Contains(e.PersonId)).ToList();
                return people.Select(Mapper.Map).ToList();
            }
        }
        // get Employee by id
        public Employee GetEmployeeById(int personId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Employees.FirstOrDefault(e => e.PersonId == personId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteEmployee(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Employees.FirstOrDefault(s => s.EmployeeId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
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

        public bool EditEmployee(Employee employee)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.People.FirstOrDefault(s => s.PersonId == employee.Person.PersonId);
                entity.FirstName = employee.Person.FirstName;
                entity.LastName = employee.Person.LastName;
                entity.Description = employee.Person.Description;
                entity.EditedBy = employee.Person.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }
    }
}
