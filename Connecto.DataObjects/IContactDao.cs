using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Contacts
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IContactDao
    {
        List<Contact> GetContacts();
        Contact GetContact(int id);
        int AddContact(Contact contact);
        bool EditContact(Contact contact);
        int DeleteContact(int id, int deletedBy);
    }
}
