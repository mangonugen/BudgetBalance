using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionDataModels;

namespace TransactionRepository.Interfaces
{
    /// <summary>
    /// IUsers DataService
    /// </summary>
    public interface IUsersDataService : IDataService, IDisposable
    {
        void RegisterUser(User user);
        User GetUserByUserName(string userName);
        User Login(string userName, string password);
        void UpdateLastLogin(User user);
        User GetUser(Guid userID);
        void UpdateUser(User user);
    }
}
