using System.Web;
using System.Web.Mvc;
using TransactionBalance.Filters;

namespace TransactionBalance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new ValidateModelStateAttribute());
        }
    }
}
