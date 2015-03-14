using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.App.ModelValidator{
    public class VendorModelValidator
    {
        private readonly Vendor _item;
        public VendorModelValidator()
        {
        }
        public VendorModelValidator(Vendor record)
        {
            _item = record;
        }

        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(_item.Name)) errors.Add(new ConnectoException { Message = "Please provide Name" });
            return errors;
        }
    }
}