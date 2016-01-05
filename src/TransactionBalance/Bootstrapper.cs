using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;
using TransactionBalance.Models;
using TransactionDataModels;

namespace TransactionBalance
{
    public class Bootstrapper
    {
        private readonly IUnityContainer _container;

        public Bootstrapper(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));            
            GlobalConfiguration.Configuration.DependencyResolver = new Microsoft.Practices.Unity.WebApi.UnityDependencyResolver(_container);

            _container.RegisterType<TransactionService.Bootstrapper>();
            var bootstrapper = _container.Resolve<TransactionService.Bootstrapper>();
            bootstrapper.Initialize();

            RegisterContainer();
            MapDtoToRepository();
            MapRepositoryToDto();
        }

        public void RegisterContainer()
        {
            //var accountInjectionConstructor = new InjectionConstructor(new TransactionDatabase());
            //_container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(accountInjectionConstructor);
            ////_container.RegisterType<IRoleStore<IdentityRole>, RoleStore<IdentityRole>>(accountInjectionConstructor);
            //_container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            //_container.RegisterType<ApplicationUserManager>();
            //_container.RegisterType<ApplicationSignInManager>();            
        }

        public void MapDtoToRepository()
        {
            Mapper.CreateMap<RegisterViewModel, User>();
            Mapper.CreateMap<LoginViewModel, User>();
        }

        public void MapRepositoryToDto()
        {
            Mapper.CreateMap<User, RegisterViewModel>();
        }
    }
}