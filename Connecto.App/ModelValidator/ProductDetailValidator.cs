using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class ProductDetailValidator
    {
        private readonly ProductDetailCart _item;
        private ProductDetailRepository _repo;
        public ProductDetailValidator()
        {
        }
        public ProductDetailValidator(ProductDetailRepository repo)
        {
            _repo = repo;
        }
        public ProductDetailValidator(ProductDetailCart record, ProductDetailRepository repo)
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