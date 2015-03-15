using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace Connecto.App.ModelValidator{
    public class MeasureValidator
    {
        private readonly Measure _item;
        private MeasureRepository _repo;
        public MeasureValidator()
        {
        }

        public MeasureValidator(MeasureRepository repo)
        {
            _repo = repo;
        }

        public MeasureValidator(Measure record, MeasureRepository repo)
        {
            _item = record;
            _repo = repo;
        }

        public List<ConnectoException> Validate()
        {
            var errors = new List<ConnectoException>();
            if (_item.Volume<=0) errors.Add(new ConnectoException { Message = "Volume Has to be a positive Number" });
            if (string.IsNullOrEmpty(_item.Lower)) errors.Add(new ConnectoException { Message = "Please provide Lower" });
            if (string.IsNullOrEmpty(_item.Actual)) errors.Add(new ConnectoException { Message = "Please provide Actual" });
            if (_repo.IsExist(_item)) errors.Add(new ConnectoException { Message = "Measure configuration already exists"});
            return errors;
        }

        public List<ConnectoException> Validate(int id)
        {
            var errors = new List<ConnectoException>();
            if (_repo.IsUsed(id)) errors.Add(new ConnectoException { Message = "Measure already used in the Product Type" });
            return errors;
        }
    }
}