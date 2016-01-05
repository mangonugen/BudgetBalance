using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionDataModels;
using TransactionRepository.Interfaces;
using TransactionService.Interfaces;

namespace TransactionService
{
    internal class AccountsBusinessRules : ValidationRules, IAccountsBusinessRules
    {
        private IAccountsDataService _accountsDataService;

        public AccountsBusinessRules(IAccountsDataService dataService)
        {
            _accountsDataService = dataService;
        }

        /// <summary>
        /// Initialize user Business Rules
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void InitializeAccountsBusinessRules(User user)
        {
            InitializeValidationRules(user);
        }

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dataService"></param>
        public void ValidateUser(User user)
        {
            InitializeValidationRules(user);

            ValidateUniqueUserName(user.UserName);
        }

        public void ValidateExistingUser(User user)
        {
            InitializeValidationRules(user);

            //require is being validate on ValidateModelState
            //ValidateRequired("FirstName", "First Name");

            ValidateUniqueUserNameForExistingUser(user.UserId, user.UserName);
        }


        /// <summary>
        /// Validate Unique User Name
        /// </summary>
        /// <param name="userName"></param>
        public void ValidateUniqueUserName(string userName)
        {
            User user = _accountsDataService.GetUserByUserName(userName);
            if (user != null)
            {
                AddValidationError("UserName", "User Name " + userName + " already exists.");
            }
        }

        /// <summary>
        /// Validate Unique User Name for Existing User
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userName"></param>
        public void ValidateUniqueUserNameForExistingUser(Guid userID, string userName)
        {
            User user = _accountsDataService.GetUserByUserName(userName);
            if (user != null)
            {
                if (user.UserId != userID)
                {
                    AddValidationError("UserName", "User Name " + userName + " already exists.");
                }
            }
        }


        /// <summary>
        /// Password Confirmation Failed
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordConfirmation"></param>
        public void ValidatePassword(string password, string passwordConfirmation)
        {

            if (passwordConfirmation.Length == 0)
                AddValidationError("PasswordConfirmation", "Password confirmation required.");

            if (password != passwordConfirmation)
            {
                AddValidationError("PasswordConfirmation", "Password confirmation failed.");
            }
        }
    }
}
