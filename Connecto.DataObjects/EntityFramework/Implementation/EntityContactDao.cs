using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using Connecto.Common.Enumeration;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IContactDao interface.
    /// </summary>
    public class EntityContactDao : IContactDao
    {
        // get all Contacts
        public List<Contact> GetContacts()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var contacts = context.Contacts.Where(e => e.Status == RecordStatus.Active).ToList();
                return contacts.Select(Mapper.Map).ToList();
            }
        }
        public List<Contact> GetContacts(int personId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var contacts = context.Contacts.Where(e => e.PersonId == personId && e.Status == RecordStatus.Active).ToList();
                return contacts.Select(Mapper.Map).ToList();
            }
        }
        // get Contact by id
        public Contact GetContact(int contactId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Contacts.FirstOrDefault(e => e.ContactId == contactId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int AddContact(Contact contact)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(contact);
                context.Contacts.Add(entity);
                context.SaveChanges();
                return entity.ContactId;
            }
        }
        public bool EditContact(Contact contact)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Contacts.FirstOrDefault(s => s.ContactId == contact.ContactId);
                entity.AddressNo = contact.AddressNo;
                entity.AddressStreet = contact.AddressStreet;
                entity.City = contact.City;
                entity.LandNumber = contact.LandNumber;
                entity.MobileNumber = contact.MobileNumber;
                entity.Province = contact.Province;
                entity.EditedBy = contact.EditedBy;
                entity.EditedOn = contact.EditedOn;
                return context.SaveChanges() > 0;
            }
        }
        public int DeleteContact(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Contacts.FirstOrDefault(s => s.ContactId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

    }
}
