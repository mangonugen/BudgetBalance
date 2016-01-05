using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionDataModels;

namespace TransactionService.Interfaces
{
    public interface IAccountsBusinessRules : IValidationRules
    {
        void ValidateUser(User user);
        void ValidateExistingUser(User user);
        void ValidatePassword(string password, string passwordConfirmation);
    }
}
