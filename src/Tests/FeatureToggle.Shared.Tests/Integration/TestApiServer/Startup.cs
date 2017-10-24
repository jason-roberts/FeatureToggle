#if NETFULL

using System.Web.Http;
using Owin;

namespace FeatureToggle.Shared.Tests.Integration.TestApiServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute("ApiWithAction", "api/{controller}/{action}");
            //config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            appBuilder.UseWebApi(config);//.UseNancy();
        }
    }
}

#endif