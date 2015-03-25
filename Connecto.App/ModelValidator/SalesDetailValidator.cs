using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class SalesDetailValidator
    {
        private readonly SalesDetailCart _item;
        private SalesDetailRepository _repo;
        public SalesDetailValidator()
        {
        }
        public SalesDetailValidator(SalesDetailRepository repo)
        {
            _repo = repo;
        }
        public SalesDetailValidator(SalesDetailCart record, SalesDetailRepository repo)
        {
            _item = record;
            _repo = repo;
        }

        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(_item.ProductCode)) errors.Add(new ConnectoException { Message = "Product code cannot be empty." });
            return errors;
        }
    }
}