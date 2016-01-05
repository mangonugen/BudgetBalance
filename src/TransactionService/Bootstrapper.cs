using Microsoft.Practices.Unity;
using TransactionService.Interfaces;

namespace TransactionService
{
    public class Bootstrapper
    {
        private readonly IUnityContainer _container;

        public Bootstrapper(IUnityContainer container)
        {
            _container = container;                        
        }

        public void Initialize()
        {
            _container.RegisterType<TransactionRepository.Bootstrapper>();
            var bootstrapper = _container.Resolve<TransactionRepository.Bootstrapper>();
            bootstrapper.Initialize();

            _container.RegisterType<IAccountsBusinessService, AccountsBusinessService>();
            _container.RegisterType<IAccountsBusinessRules, AccountsBusinessRules>();
        }
    }
}
