using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class ReturnReasonValidator
    {
        private readonly ReturnReason _item;
        private ReturnReasonRepository _repo;
        public ReturnReasonValidator()
        {
        }
        public ReturnReasonValidator(ReturnReasonRepository repo)
        {
            _repo = repo;
        }
        public ReturnReasonValidator(ReturnReason record, ReturnReasonRepository repo)
        {
            _item = record;
            _repo = repo;
        }

        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(_item.Name)) errors.Add(new ConnectoException { Message = "Please provide Name" });
            if (_repo.IsExist(_item)) errors.Add(new ConnectoException { Message = "Vendor Name already exists"});
            return errors;
        }

        public List<ConnectoException> Validate(int id)
        {
            var errors = new List<ConnectoException>();
            if (_repo.IsUsed(id)) errors.Add(new ConnectoException { Message = "Vendor already used in the product" });
            return errors;
        }
    }
}