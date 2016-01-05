using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionRepository.Interfaces
{
    /// <summary>
    /// IDataService
    /// </summary>
    public interface IDataService
    {
        void CreateSession();
        void BeginTransaction();
        Task CommitTransaction(Boolean closeSession);
        void RollbackTransaction(Boolean closeSession);
        void CloseSession();
    }
}
