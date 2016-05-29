using System;
using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface IWriteoffDetailDao
    {

        Tuple<IList<WriteoffDetail>, int> GetListofWriteoffDetails(FilterCriteria filter);

        //IList<WriteoffDetail> GetListofWriteoffDetails();

        List<Writeoff> GetWriteoffs(bool writeoff);
        List<WriteoffDetail> GetWriteoffDetails(int writeoffId);
        List<WriteoffDetail> GetWriteoffDetail(string productCode);
        List<WriteoffDetailCart> GetWriteoffDetailsCart(int WriteoffId);
        int AddWriteoffDetailCart(WriteoffDetailCart writeoffDetailCart);
        int AddWriteoffDetail(int WriteoffId);
        int DeleteWriteoffDetailCart(int id, int deletedBy);
        bool UpdateWriteoff(WriteoffDetailCart writeoffDetailCart);
        bool EditWriteoffDetailCart(WriteoffDetailCart writeoffDetailCart);
        List<WriteoffDetail> GetWriteoffedWriteoffDetailsCart(int writeoffId);
        int DeleteWriteoffDetails(int id, int deletedBy);

    }
}
