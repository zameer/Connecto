using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class PersonRepository
    {
        private static readonly IPersonDao PersonDao = DataAccess.PersonDao;

        public IList<Person> GetAll()
        {
            return PersonDao.GetPeople();
        }
        public Person GetVendorById(int id)
        {
            return PersonDao.GetById(id);
        }
        public int Delete(int id, int deletedBy)
        {
            return PersonDao.DeletePerson(id, deletedBy);
        }
        public void Add(Person person)
        {
            PersonDao.AddPerson(person);
        }
        public void Edit(Person person)
        {
            PersonDao.EditPerson(person);
        }

        public bool IsUsed(int id)
        {
            return PersonDao.IsUsed(id);
        }
    }
}