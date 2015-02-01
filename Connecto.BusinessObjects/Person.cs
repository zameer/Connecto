using System;
using System.Collections.Generic;

namespace Connecto.BusinessObjects
{
    public class Person : Connecto
    {
        public int PersonId { get; set; }
        public Guid PersonGuid { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName
        {
            get { return FirstName + " " + LastName; }
        }
        public IList<Contact> Contact { get; set; }
    }
}
