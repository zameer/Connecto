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
        /// Get List of Contact
        /// </summary>
        /// <param name="personId">by person Id</param>
        /// <returns>IList of Contact</returns>
        public IList<Contact> GetAll(int personId)
        {
            return ContactDao.GetContacts(personId);
        }

        /// <summary>
        /// Get a specific Contact
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Contact ID</returns>
        public Contact GetContact(int id)
        {
            return ContactDao.GetContact(id);
        }

        /// <summary>
        /// Removes specific Contact
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of Contact Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return ContactDao.DeleteContact(id, deletedBy);
        }

        /// <summary>
        /// Create new Contact
        /// </summary>
        /// <param name="vendor">Create Contact object</param>
        public void Add(Contact contact)
        {
            ContactDao.AddContact(contact);
        }
        public void Edit(Contact contact)
        {
            ContactDao.EditContact(contact);
        }
    }
}