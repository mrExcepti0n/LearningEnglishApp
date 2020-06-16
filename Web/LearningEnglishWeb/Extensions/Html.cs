using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglishWeb.Extensions
{
    public static class Html
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string controller)
        {
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            if (!currentController.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return "active";
        }
    }
}
