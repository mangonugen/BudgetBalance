using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionDataModels;

namespace TransactionService.Interfaces
{
    /// <summary>
    /// IAccounts DataService
    /// </summary>
    public interface IUsersBusinessService
    {
        Task<User> RegisterUser(User user, string confirmationPassword, TransactionInformation transaction);
        Task<User> Login(User user, TransactionInformation transaction);
    }
}
