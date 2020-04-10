using Autofac;
using Autofac.Integration.WebApi;
using SpeechApi;
using SpeechApi.Models;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SpeachApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterWebApiModelBinderProvider();
            builder.RegistryServices();         

         


            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }


    public static class SearchApiExtenstions
    {
        public static ContainerBuilder RegistryServices(this ContainerBuilder container)
        {
            container.RegisterType<TextToSpeech>();
            container.RegisterType<SpeechToText>();
            return container;
        } 
    }
}
