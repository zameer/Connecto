using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class ContactRepository
    {
        private static readonly IContactDao ContactDao = DataAccess.ContactDao;

        /// <summary>
        /// Get List of Contact
        /// </summary>
        /// <returns>IList of Contact</returns>
        public IList<Contact> GetAll()
        {
            return ContactDao.GetContacts();
        }

        /// <summary>
        /// Get a specific Contact
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Contact ID</returns>
        public Contact GetContactById(int id)
        {
            return ContactDao.GetContact(id);
        }

        /// <summary>
        /// Removes specific Contact
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of Contact Deleted</returns>
        public int Delete(int id = 0)
        {
            return ContactDao.DeleteContact(id);
        }

        /// <summary>
        /// Create new Contact
        /// </summary>
        /// <param name="vendor">Create Contact object</param>
        public void Add(Contact contact)
        {
            ContactDao.AddContact(contact);
        }
    }
}