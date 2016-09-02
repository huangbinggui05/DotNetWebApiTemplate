using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CaseBoardWebApi.Controllers.Example;
using CaseBoardWebApi.Infrastructure.Example;
using CaseBoardWebApi.Models.Example;
using WebApiContrib.Formatting.CollectionJson.Client;

namespace CaseBoardWebApi
{
    public static class WebApiConfiguration
    {
        public static void Configure(HttpConfiguration config, IIssueStore issueStore = null)
        {
            //api自描述接口路由
            config.Routes.MapHttpRoute("Root", "", new { controller = "Home"});

            //默认路由 uri中api对应RoutePrefix特性，{controller}对应Route特性
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            //支持基于属性的路由 
            config.MapHttpAttributeRoutes();

            //开启自定义操作筛选器
            config.Filters.Add(new MyFilters.MyActionFilter());

            //开启自定义授权筛选器
            config.Filters.Add(new MyFilters.MyAuthorizationFilter());

            ConfigureFormatters(config);
            ConfigureAutofac(config, issueStore);
            EnableCors(config);
        }
       
        private static void ConfigureFormatters(HttpConfiguration config)
        {
            config.Formatters.Add(new CollectionJsonFormatter());
            JsonSerializerSettings settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.issue+json"));
        }

        public static void ConfigureAutofac(HttpConfiguration config, IIssueStore issueStore)
        {
            var builder = new ContainerBuilder();
         
            builder.RegisterApiControllers(typeof(IssueController).Assembly);

            if (issueStore == null)
                builder.RegisterType<InMemoryIssueStore>().As<IIssueStore>().SingleInstance();
            else
                builder.RegisterInstance(issueStore);

            builder.RegisterType<IssueStateFactory>().As<IStateFactory<Issue, IssueState>>().InstancePerLifetimeScope();
            builder.RegisterType<IssueLinkFactory>().InstancePerLifetimeScope();
            builder.RegisterHttpRequestMessage(config);
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static void EnableCors(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}
