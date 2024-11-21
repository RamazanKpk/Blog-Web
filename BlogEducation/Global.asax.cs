using Autofac;
using Autofac.Integration.WebApi;
using BlogEducation.OAuth;
using Buisenss.Interface.Abstract;
using Buisenss.Interface.Abstract.ApiAbstract;
using Buisenss.Interface.ApiServices;
using Buisenss.Interface.Service;
using DataModel.ArticleCommentModels;
using DataModel.ArticleModels;
using DataModel.CategoryModels;
using Entity.Concrate;
using Microsoft.Owin.Builder;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogEducation
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.EnableCors();

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterType<ArticleCommentApiService>().As<IAllApiServices<ArticleCommentModel, CreateCommentModel, CommentDetailListModel>>();
            builder.RegisterType<ArticleApiService>().As<IAllApiServices<ArticleListModel, CreateArticleListModel, ArticleDetailListModel>>();
            builder.RegisterType<CategoryApiService>().As<IAllApiServices<CategoryListModel, Category,CategoryListModel>>();
            builder.RegisterType<UserService>().As<IUserService>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var startup = new Startup();
            //startup.ConfigureOAuth(new AppBuilder());
            startup.Configration(new AppBuilder());

        }

    }
}