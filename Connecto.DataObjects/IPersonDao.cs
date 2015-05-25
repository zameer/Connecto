using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Vendor
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IPersonDao
    {
        Tuple<IList<Person>, int> GetPeople(FilterCriteria filter);
        IList<Person> GetPeople();
        Person GetById(int id);
        int DeletePerson(int id, int deletedBy);
        int AddPerson(Person person);
        bool EditPerson(Person person);
        bool IsUsed(int id);
    }
}
