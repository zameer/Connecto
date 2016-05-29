using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;
using Connecto.DataObjects.EntityFramework.Utility;

namespace Connecto.Repositories
{
    public class WriteoffDetailRepository
    {
        private static readonly IWriteoffDetailDao Repo = DataAccess.WriteoffDetailDao;


        public Tuple<IList<WriteoffDetail>, int> GetDetailList(FilterCriteria filter)
        {
            return Repo.GetListofWriteoffDetails(filter);
        }





        public IList<Writeoff> GetWriteoffs(bool writeoff)
        {
            return Repo.GetWriteoffs(writeoff);
        }
        
        public IList<WriteoffDetail> GetAll(int writeoffId)
        {
            return Repo.GetWriteoffDetails(writeoffId);
        }
        public List<WriteoffDetail> GetWriteoffDetail(string productCode)
        {
            return Repo.GetWriteoffDetail(productCode);
        }

        public IList<WriteoffDetailCart> GetCart(int ProductDetailId)
        {
            return Repo.GetWriteoffDetailsCart(ProductDetailId);
        }

        public int AddToCart(WriteoffDetailCart writeoffDetailCart)
        {
            return Repo.AddWriteoffDetailCart(writeoffDetailCart);
        }

        public int Add(int WriteoffId)
        {
            return Repo.AddWriteoffDetail(WriteoffId);
        }

        public int Delete(int id, int deletedBy)
        {
            return Repo.DeleteWriteoffDetailCart(id, deletedBy);
        }

        public int DeleteWriteoffDetail(int id, int deletedBy)
        {
            return Repo.DeleteWriteoffDetails(id, deletedBy);
        }

        public bool UpdateWriteoff(WriteoffDetailCart writeoffDetailCart)
        {
            return Repo.UpdateWriteoff(writeoffDetailCart);
        }
        
    }
}