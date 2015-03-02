using System.Collections.Generic;
using Connecto.Common.Enumeration;
using System;

namespace Connecto.BusinessObjects
{
    public class ConnectoValidation
    {
        public bool IsValid { get; set; }
        public string Status { get; set; }
        public Exception Exception { get; set; }
        public List<ConnectoException> Exceptions { get; set; }
    }

    public class ConnectoException
    {
        public string Message { get; set; }
    }
}
