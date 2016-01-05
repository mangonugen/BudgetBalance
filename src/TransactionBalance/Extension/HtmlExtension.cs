using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TransactionBalance.Extension
{
    public static class HtmlExtension
    {
        /// <summary>
        /// Renders the styles tag with optional html attributes.
        /// </summary>
        /// <param name="path">
        /// The virtual path of the styles.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        /// <example>
        /// @Html.RenderStyles("~/Css/Spaces", new {media = "print"})
        /// <link href="/Css/Spaces?v=OVeDyrP68xENlN98" rel="stylesheet" media="print"/>
        /// </example>
        public static IHtmlString RenderStyles(this HtmlHelper htmlHelper, string path, object htmlAttributes)
        {
            var attributes = BuildHtmlStringFrom(htmlAttributes);

            string completedTag = string.Empty;

#if DEBUG

            var originalHtml = Styles.Render(path).ToHtmlString();
            completedTag = originalHtml.Replace("/>", attributes + "/>");
#else
            completedTag = string.Format(
                "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\"{1} />",
                Styles.Url(path), attributes);

#endif

            return MvcHtmlString.Create(completedTag);
        }

        /// <summary>
        /// Renders the scripts tag with optional html attributes.
        /// </summary>
        /// <param name="path">The virtual path of the scripts.</param>
        /// <param name="htmlAttributes">The html attributes.</param>
        /// <returns>The <see cref="IHtmlString"/>.</returns>
        /// <example>
        /// @Html.RenderScripts("~/Js/Spaces", new { async = "true" })
        /// <script src="/Js/Spaces?v=IJe2dZiudhgHX-5go2gdHoyVzF1zdP6jlBYm2g_OYU41" async="true" />        
        /// </example>
        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper, string path, object htmlAttributes)
        {
            var attributes = BuildHtmlStringFrom(htmlAttributes);

            string completedTag = string.Empty;

#if DEBUG

            var originalHtml = Scripts.Render(path).ToHtmlString();
            completedTag = originalHtml.Replace("/>", attributes + "/>");
#else
            completedTag = string.Format(
                "<script src=\"{0}\" {1} />",
                Scripts.Url(path), attributes);

#endif

            return MvcHtmlString.Create(completedTag);
        }

        #region private helper methods
        /// <summary>
        /// Merge 2 html attributes object together and return dictionary
        /// </summary>
        /// <param name="obj1">First object</param>
        /// <param name="obj2">Second object</param>
        /// <returns>Html Attributes</returns>
        private static Dictionary<string, object> MergeAttributes(object obj1, object obj2)
        {
            BindingFlags publicAttributes = BindingFlags.Public | BindingFlags.Instance;
            Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

            foreach (PropertyInfo property in obj1.GetType().GetProperties(publicAttributes))
            {
                if (property.CanRead)
                {
                    string propName = property.Name.Replace('_', '-');
                    htmlAttributes.Add(propName, property.GetValue(obj1, null));
                }
            }

            foreach (PropertyInfo property in obj2.GetType().GetProperties(publicAttributes))
            {
                if (property.CanRead)
                {
                    string propName = property.Name.Replace('_', '-');
                    htmlAttributes.Add(propName, property.GetValue(obj2, null));
                }
            }

            return htmlAttributes;
        }

        /// <summary>
        /// Merge 2 html attributes object together and return RouteValueDictionary
        /// </summary>
        /// <param name="obj1">First object</param>
        /// <param name="obj2">Second object</param>
        /// <returns>Html Attributes</returns>
        private static RouteValueDictionary ConcatAttributes(object obj1, object obj2)
        {
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(obj1);
            foreach (var item in HtmlHelper.AnonymousObjectToHtmlAttributes(obj2))
            {
                htmlAttributes.Add(item.Key, item.Value);
            }

            return htmlAttributes;
        }

        /// <summary>
        /// Use the html attributes and loop through in order
        /// to add to the completed tag.
        /// </summary>
        /// <param name="htmlAttributes">The html attributes.</param>
        /// <returns>An HTML string containing the html attributes</returns>
        private static string BuildHtmlStringFrom(object htmlAttributes)
        {
            // Try and safely cast
            var routeHtmlAttributes = htmlAttributes as IDictionary<string, object> ?? new RouteValueDictionary(htmlAttributes);

            var attributeBuilder = new StringBuilder();

            foreach (var attribute in routeHtmlAttributes)
            {
                attributeBuilder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
            }

            return attributeBuilder.ToString();
        }
        #endregion
    }
}