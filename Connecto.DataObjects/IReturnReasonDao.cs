using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access ReturnReason
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IReturnReasonDao
    {
        Tuple<IList<ReturnReason>, int> GetReturnReasonsSearch(FilterCriteria filter);
        IList<ReturnReason> GetReturnReasons();
        ReturnReason GetReturnReasonById(int id);
        int DeleteReturnReason(int id, int deletedBy);
        int AddReturnReason(ReturnReason returnReason);
        bool EditReturnReason(ReturnReason returnReason);
        bool IsExist(ReturnReason returnReason);
        bool IsUsed(int id);
    }
}
