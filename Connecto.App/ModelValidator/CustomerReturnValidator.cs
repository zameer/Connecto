using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class CustomerReturnValidator
    {
        private readonly SalesDetail _item;
        private CustomerReturnRepository _repo;
        public CustomerReturnValidator()
        {
        }
        public CustomerReturnValidator(CustomerReturnRepository repo)
        {
            _repo = repo;
        }
        public CustomerReturnValidator(SalesDetail record, CustomerReturnRepository repo)
        {
            _item = record;
            _repo = repo;
        }
        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (_item.InvoiceId == 0) errors.Add(new ConnectoException { Message = "Please provide Invoice No" });
            return errors;
        }
        public List<ConnectoException> Validate(int id)
        {
            var errors = new List<ConnectoException>();
            if (_repo.IsUsed(id)) errors.Add(new ConnectoException { Message = "Invoice No Not Available." });
            return errors;
        }
    }
}