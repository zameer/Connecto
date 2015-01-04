using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.BusinessObjects
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
