using Microsoft.Practices.Unity;
using TransactionRepository;
using TransactionRepository.Interfaces;

namespace TransactionRepository
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
            _container.RegisterType<IUsersDataService, UsersDataService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAccountsDataService, AccountsDataService>(new ContainerControlledLifetimeManager());
        }
    }
}
