using System;
using System.Threading.Tasks;
using TransactionDataModels;
using TransactionRepository.Interfaces;
using TransactionService.Interfaces;

namespace TransactionService
{
    public class AccountsBusinessService : IAccountsBusinessService
    {
        private readonly IAccountsDataService _accountDataService;
        private readonly IAccountsBusinessRules _businessRules;

        public AccountsBusinessService(IAccountsDataService accountDataService, IAccountsBusinessRules businessRules)
        {
            _accountDataService = accountDataService;
            _businessRules = businessRules;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmationPassword"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<User> RegisterUser(User user, string confirmationPassword, TransactionInformation transaction)
        {
            try
            {
                user.FirstName = Utilities.UppercaseFirstLetter(user.FirstName.Trim());
                user.LastName = Utilities.UppercaseFirstLetter(user.LastName.Trim());

                _accountDataService.CreateSession();

                _businessRules.ValidateUser(user);
                _businessRules.ValidatePassword(user.Password, confirmationPassword);

                if (_businessRules.ValidationStatus == true)
                {
                    _accountDataService.BeginTransaction();
                    _accountDataService.RegisterUser(user);
                    await _accountDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                    transaction.ReturnMessage.Add("User registered successfully.");
                }
                else
                {
                    transaction.ReturnStatus = _businessRules.ValidationStatus;
                    transaction.ReturnMessage = _businessRules.ValidationMessage;
                    transaction.ValidationErrors = _businessRules.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                transaction.AddExceptionMessage(ex);
            }
            finally
            {
                _accountDataService.CloseSession();
            }

            return user;
        }

        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<User> Login(User user, TransactionInformation transaction)
        {
            try
            {
                _accountDataService.CreateSession();
                user = _accountDataService.Login(user.UserName, user.Password);

                if (user != null)
                {
                    _accountDataService.BeginTransaction();
                    _accountDataService.UpdateLastLogin(user);
                    await _accountDataService.CommitTransaction(true);
                    transaction.ReturnStatus = true;
                }
                else
                {
                    transaction.ReturnStatus = false;
                    transaction.ReturnMessage.Add("Invalid login or password.");
                }
            }
            catch (Exception ex)
            {
                transaction.AddExceptionMessage(ex);
            }
            finally
            {
                _accountDataService.CloseSession();
            }

            return user;
        }
    }
}
