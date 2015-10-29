using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class WriteoffDetailValidator
    {
        private readonly WriteoffDetailCart _item;
        private WriteoffDetailRepository _repo;
        public WriteoffDetailValidator()
        {
        }
        public WriteoffDetailValidator(WriteoffDetailRepository repo)
        {
            _repo = repo;
        }
        public WriteoffDetailValidator(WriteoffDetailCart record, WriteoffDetailRepository repo)
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