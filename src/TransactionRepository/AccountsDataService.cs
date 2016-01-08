using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TransactionRepository.Interfaces;

namespace TransactionRepository
{
    public class AccountsDataService : EntityFrameworkDataService, IAccountsDataService
    {
    }
}
