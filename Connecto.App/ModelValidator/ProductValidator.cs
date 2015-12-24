using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class ProductValidator
    {
        private readonly Product _item;
        private ProductRepository _repo;
        public ProductValidator()
        {
        }

        public ProductValidator(ProductRepository repo)
        {
            _repo = repo;
        }

        public ProductValidator(Product record, ProductRepository repo)
        {
            _item = record;
            _repo = repo;
        }

        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(_item.Name)) errors.Add(new ConnectoException { Message = "Please provide Name" });
            if (_repo.IsExist(_item)) errors.Add(new ConnectoException { Message = "Product Name already exists" });
            return errors;
        }

        public List<ConnectoException> Validate(int id)
        {
            var errors = new List<ConnectoException>();
            if (_repo.IsUsed(id)) errors.Add(new ConnectoException { Message = "Product already used with purchasing" });
            return errors;
        }
    }
}