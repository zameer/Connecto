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
            if (_item.OrderId == 0  ) errors.Add(new ConnectoException { Message = "Invoice No cannot be empty." });
            return errors;
        }
    }
}