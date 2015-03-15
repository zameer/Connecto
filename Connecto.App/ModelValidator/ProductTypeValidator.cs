using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class ProductTypeValidator
    {
        private readonly ProductType _item;
        private ProductTypeRepository _repo;
        public ProductTypeValidator()
        {
        }

        public ProductTypeValidator(ProductTypeRepository repo)
        {
            _repo = repo;
        }

        public ProductTypeValidator(ProductType record, ProductTypeRepository repo)
        {
            _item = record;
            _repo = repo;
        }

        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (_item.MeasureId<=0) errors.Add(new ConnectoException { Message = "Please seect Measure" });
            if (string .IsNullOrEmpty(_item.Type)) errors.Add(new ConnectoException { Message = "Please provide Type" });
            if (_repo.IsExist(_item)) errors.Add(new ConnectoException { Message = "Product Type already exists"});
            return errors;
        }

        public List<ConnectoException> Validate(int id)
        {
            var errors = new List<ConnectoException>();
            if (_repo.IsUsed(id)) errors.Add(new ConnectoException { Message = "Product type already used in the product" });
            return errors;
        }
    }
}