using System;

namespace Connecto.BusinessObjects
{
    public class Contact : Connecto
    {
        public int ContactId { get; set; }
        public Guid ContactGuid { get; set; }
        public int EmployeeId { get; set; }
        public string LandNumber { get; set; }
        public string MobileNumber { get; set; }
        public string AddressNo { get; set; }
        public string AddressStreet { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}