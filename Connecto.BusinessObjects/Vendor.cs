﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.BusinessObjects
{
    public class Vendor : Connecto
    {
        public int VendorId { get; set; }
        public Guid VendorGuid { get; set; }
        public string Name { get; set; }
    }
}
