using Boma.Rs.Api.StartupConfigurations;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Boma.Rs.Api.Startup))]

namespace Boma.Rs.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = StartupHttpConfiguration.CreateHttpConfiguration(app);

            // Autenticação
            ConfigureAuth(app);

            // Cross-Domain
            app.UseCors(CorsOptions.AllowAll);
            

            // Injeção de dependência(Simple Injector)
            //app.ConfigureContainer(httpConfiguration);
        }

       
    }

    public static partial class StartupHttpConfiguration
    {
        public static HttpConfiguration CreateHttpConfiguration(this IAppBuilder appBuilder)
        {

            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };


            var formatters = config.Formatters;
            formatters.Add(new BsonMediaTypeFormatter());

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Mapea Rotas
            config.MapHttpAttributeRoutes();

            //// adiciona os filters ao pipeline
            config.Filters.Add(new AuthorizeAttribute());


            return config;
        }
    }
}
