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

        /// <summary>
        /// Remove specific Contact
        /// </summary>
        /// <param name="id">Unique Contact identifier</param>
        /// <returns>No of Contact Deleted</returns>
        int DeleteContact(int id = 0);
    }
}
