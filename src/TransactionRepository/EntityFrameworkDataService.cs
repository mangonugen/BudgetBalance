using System;
using System.Data.Entity;
using System.Threading.Tasks;
using TransactionRepository.Interfaces;
using TransactionRepository.Migrations;

namespace TransactionRepository
{    
    public class EntityFrameworkDataService : IDataService, IDisposable
    {

        TransactionDatabase _connection;

        public TransactionDatabase dbConnection
        {
            get { return _connection; }
        }

        public async Task CommitTransaction(Boolean closeSession)
        {
            //dbConnection.SaveChanges();
            await dbConnection.SaveChangesAsync();
        }

        public void RollbackTransaction(Boolean closeSession)
        {

        }

        public void Save(object entity) { }

        public void CreateSession()
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TransactionDatabase, Configuration>());

            if (_connection == null)
            {
                _connection = new TransactionDatabase();
            }
        }
        public void BeginTransaction() { }

        public void CloseSession() { }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
        }
    }
}
