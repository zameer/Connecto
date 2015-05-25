using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IVendorDao interface.
    /// </summary>
    public class EntityPersonDao : IPersonDao
    {
        public Tuple<IList<Person>, int>  GetPeople(FilterCriteria filter)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                List<Person> items;
                var count = context.People.Count();
                if(!string.IsNullOrEmpty(filter.sSearch))
                {
                    count = context.People.Count(e => e.FirstName.ToLower().Contains(filter.sSearch) || e.LastName.ToLower().Contains(filter.sSearch));
                    items = context.People.Where(e => e.FirstName.ToLower().Contains(filter.sSearch) || e.LastName.ToLower().Contains(filter.sSearch))
                        .OrderBy(e => e.PersonId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                else
                {
                    items = context.People.OrderBy(e => e.PersonId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                return new Tuple<IList<Person>, int>(items, count);
            }
        }
        public IList<Person> GetPeople()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var people = context.People.ToList();
                return people.Select(Mapper.Map).ToList();
            }
        }
        public Person GetById(int personId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.People.FirstOrDefault(e => e.PersonId == personId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeletePerson(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.People.FirstOrDefault(s => s.PersonId == id);
                context.People.Remove(entity);
                return context.SaveChanges();
            }
        }

        public int AddPerson(Person person)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(person);
                context.People.Add(entity);
                context.SaveChanges();
                return entity.PersonId;
            }
        }

        public bool EditPerson(Person person)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.People.FirstOrDefault(s => s.PersonId == person.PersonId);
                entity.Description = person.Description;
                entity.FirstName = person.FirstName;
                entity.LastName = person.LastName;
                entity.EditedBy = person.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }

        public bool IsUsed(int id) {
            using (var context = DataObjectFactory.CreateContext())
            {
                var isUsed = context.Employees.Any(s => s.PersonId == id && s.Status == Common.Enumeration.RecordStatus.Active);
                if (!isUsed) isUsed = context.Suppliers.Any(s => s.PersonId == id && s.Status == Common.Enumeration.RecordStatus.Active);
                return isUsed;
            }
        }
    }
}
