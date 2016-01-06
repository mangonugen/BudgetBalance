using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TransactionBalance.Infrastructure
{
    public class Utilities
    {
        /// <summary>
        /// Render partial view. Need to implement with option boolean datatype for isPartial
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName"></param>
        /// <param name="viewData"></param>
        /// <returns></returns>
        ///http://weblog.west-wind.com/posts/2013/Jul/15/Rendering-ASPNET-MVC-Razor-Views-outside-of-MVC-revisited
        public static string ViewRenderer<T>(string viewName, object viewData)
            where T : Controller, new()
        {
            T controller;
            ControllerContext controllerContext;

            if (HttpContext.Current != null)
            {
                controller = CreateController<T>();
            }
            else
            {
                throw new InvalidOperationException(
                    "ViewRenderer must run in the context of an ASP.NET " +
                    "Application and requires HttpContext.Current to be present.");
            }

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Creates an instance of an MVC controller from scratch 
        /// when no existing ControllerContext is present       
        /// </summary>
        /// <typeparam name="T">Type of the controller to create</typeparam>
        /// <returns>Controller Context for T</returns>
        /// <exception cref="InvalidOperationException">thrown if HttpContext not available</exception>
        private static T CreateController<T>(RouteData routeData = null)
                    where T : Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();

            // get context wrapper from HttpContext if available
            HttpContextBase wrapper = null;
            if (HttpContext.Current != null)
            {
                wrapper = new HttpContextWrapper(HttpContext.Current);
            }
            else
            {
                throw new InvalidOperationException(
                    "Can't create Controller Context if no active HttpContext instance is available.");
            }

            if (routeData == null)
            {
                routeData = new RouteData();
            }

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
            {
                routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", string.Empty));
            }

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }
    }
}